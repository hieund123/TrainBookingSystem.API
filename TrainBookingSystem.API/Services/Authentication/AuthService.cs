using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TrainBookingSystem.API.Models;
using TrainBookingSystem.API.Models.Authentication.Login;
using TrainBookingSystem.API.Models.Authentication.Password;
using TrainBookingSystem.API.Models.Authentication.SignUp;
using TrainBookingSystem.Service.Models;
using TrainBookingSystem.Service.Services;

namespace TrainBookingSystem.API.Services.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<IdentityUser> userManager,
                           SignInManager<IdentityUser> signInManager,
                           RoleManager<IdentityRole> roleManager,
                           IEmailService emailService,
                           IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailService = emailService;
            _configuration = configuration;
        }


        public async Task<Response> RegisterAsync([FromBody] RegisterUserDto registerUser, string role)
        {
            // Check user exist
            var userExist = await _userManager.FindByEmailAsync(registerUser.Email!);
            if (userExist != null)
            {
                return new Response { Status = "Error", Message = "User already exist." };
            }

            // Add the user in database
            IdentityUser user = new()
            {
                Email = registerUser.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registerUser.Username,
                TwoFactorEnabled = true
            };
            var result = await _userManager.CreateAsync(user, registerUser.Password!);
            if (!result.Succeeded)
            {
                return new Response { Status = "Error", Message = "User creation failed." };
            }

            // Check role
            if (!await _roleManager.RoleExistsAsync(role))
            {
                return new Response { Status = "Error", Message = $"Role {role} does not exist." };
            }

            // Asign a role
            await _userManager.AddToRoleAsync(user, role);

            // Generate email confirmation token and send email
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var clientUrl = _configuration["AppSettings:ClientUrl"];
            var confirmationLink = $"{clientUrl}/api/auth/ConfirmEmail?token={Uri.EscapeDataString(token)}&email={user.Email}";

            var message = new Message(new string[] { user.Email! }, "Confirmation email link", confirmationLink);
            try
            {
                _emailService.sendEmail(message);
                Console.WriteLine($"📧 Sent confirmation link to {user.Email}: {confirmationLink}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Failed to send confirmation email to {user.Email}: {ex.Message}");
                return new Response { Status = "Error", Message = $"User created but failed to send confirmation email: {ex.Message}" };
            }

            return new Response { Status = "Success", Message = $"User created & email sent to {user.Email} Successfully" };
        }

        public async Task<AuthResponse> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email!);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password!))
            {
                return new AuthResponse { Status = "Error", Message = "Invalid Email or Password" };
            }

            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                return new AuthResponse { Status = "Error", Message = "Email not confirmed" };
            }

            var userRole = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var role in userRole)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            if (user.TwoFactorEnabled)
            {
                await _signInManager.SignOutAsync();
                await _signInManager.PasswordSignInAsync(user.UserName!, loginDto.Password!, false, true);

                var token = await _userManager.GenerateTwoFactorTokenAsync(user, "email");

                var message = new Message(new string[] { user.Email! }, "OTP confirmation", token);
                _emailService.sendEmail(message);

                return new AuthResponse { Status = "Success", Message = $"We have sent an OTP to your email {user.Email}" };
            }

            var jwtToken = GetToken(authClaims);

            return new AuthResponse
            {
                Status = "Success",
                Message = "Login successfull",
                Token = new JwtSecurityTokenHandler().WriteToken(jwtToken)
            };
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]!));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                expires: DateTime.UtcNow.AddDays(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return token;
        }

        public JwtSecurityToken GenerateJwtToken(IdentityUser user, IList<string> roles)
        {
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var role in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            return GetToken(authClaims);
        }

        public async Task<Response> ForgotPasswordAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                return new Response { Status = "Error", Message = "Invalid request" };
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var clientUrl = _configuration["AppSettings:ClientUrl"];
            //var resetLink = $"{clientUrl}/reset-password?email={Uri.EscapeDataString(email)}&token={Uri.EscapeDataString(token)}";
            var resetLink = $"{clientUrl}/api/Auth/reset-password?email={email}&token={Uri.EscapeDataString(token)}";


            var message = new Message(new string[] { email }, "Reset Password", $"Click the link to reset your password: {resetLink} ");
            try
            {
                _emailService.sendEmail(message);
                return new Response { Status = "Success", Message = "Reset password email sent." };
            }
            catch (Exception ex)
            {
                return new Response { Status = "Error", Message = $"Failed to send email: {ex.Message}" };
            }
        }

        public async Task<Response> ResetPasswordAsync(ResetPasswordDto model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                return new Response { Status = "Error", Message = "Password and Confirm Password do not match." };
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return new Response { Status = "Error", Message = "User not found." };
            }
            string decodedToken = Uri.UnescapeDataString(model.Token);
            var result = await _userManager.ResetPasswordAsync(user, decodedToken, model.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(" | ", result.Errors.Select(e => e.Description));
                return new Response { Status = "Error", Message = errors };
            }

            return new Response { Status = "Success", Message = "Password reset successful." };
        }


    }
}
