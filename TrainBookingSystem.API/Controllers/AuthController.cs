using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TrainBookingSystem.API.Models.Authentication.Login;
using TrainBookingSystem.API.Models.Authentication.Password;
using TrainBookingSystem.API.Models.Authentication.SignUp;
using TrainBookingSystem.API.Services.Authentication;
using TrainBookingSystem.Service.Models;
using TrainBookingSystem.Service.Services;
using TrainBookingSystem.API.Models;

namespace TrainBookingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IEmailService _emailService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public AuthController(IAuthService authService, IEmailService emailService, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
                            RoleManager<IdentityRole> roleManager)
        {
            _authService = authService;
            _emailService = emailService;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;

        }

        //[HttpGet("test-send-email")]
        //public IActionResult TestSendEmail()
        //{
        //    var message = new Message(
        //        new[] { "ghechuoi1@gmail.com" },
        //        "Test Subject",
        //        "This is a test email."
        //    );

        //    try
        //    {
        //        _emailService.sendEmail(message);
        //        return Ok("Email sent.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest("Failed to send email: " + ex.ToString());
        //    }
        //}

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUser, string role)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                                       .SelectMany(x => x.Errors)
                                       .Select(x => x.ErrorMessage)
                                       .ToList();

                return BadRequest(new Response
                {
                    Status = "Error",
                    Message = string.Join(" | ", errors)
                });
            }

            var response = await _authService.RegisterAsync(registerUser, role);
            if (response.Status == "Error")
            {
                return BadRequest(response);
            }

            return StatusCode(StatusCodes.Status201Created, response);
        }

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Message = "Email Verified Successfully." });
                }
                else
                {
                    return BadRequest(new Response { Status = "Error", Message = "Invalid token or token expired." });
                }
            }
            return NotFound(new Response { Status = "Error", Message = "This Email does not exist." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Response
                {
                    Status = "Error",
                    Message = "Invalid login data"
                });
            }

            var result = await _authService.LoginAsync(loginDto);
            if (result.Status == "Error")
            {
                return Unauthorized(result);
            }
            return Ok(result);
        }

        [HttpPost("login-2fa")]
        public async Task<IActionResult> LoginWithOTP(string code, string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            var signIn = await _signInManager.TwoFactorSignInAsync("Email", code, false, false);
            if (signIn.Succeeded)
            {
                if (user != null)
                {
                    var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserName!),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    };
                    var userRoles = await _userManager.GetRolesAsync(user);
                    foreach (var role in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, role));
                    }

                    var roles = await _userManager.GetRolesAsync(user);
                    var jwtToken = _authService.GenerateJwtToken(user, roles);

                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                        expiration = jwtToken.ValidTo
                    });
                }
            }

            return StatusCode(StatusCodes.Status404NotFound,
                new Response { });
        }

        // Send reset-password-email with email and token
        [HttpPost("forgot-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto model)
        {
            var response = await _authService.ForgotPasswordAsync(model.Email);
            if (response.Status == "Error") return BadRequest(response);
            return Ok(response);
        }

        // Perform reset password
        [HttpPost("reset-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto model)
        {
            var response = await _authService.ResetPasswordAsync(model);
            if (response.Status == "Error") return BadRequest(response);
            return Ok(response);
        }

        //[HttpGet("reset-password")]
        //public IActionResult ResetPasswordLink([FromQuery] string email, [FromQuery] string token)
        //{
        //    return Ok(new { email, token });
        //}

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok(new { Status = "Success", Message = "Logout successful" });
        }

    }
}
