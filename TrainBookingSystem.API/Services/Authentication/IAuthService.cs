using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using TrainBookingSystem.API.Models;
using TrainBookingSystem.API.Models.Authentication.Login;
using TrainBookingSystem.API.Models.Authentication.Password;
using TrainBookingSystem.API.Models.Authentication.SignUp;
using TrainBookingSystem.API.Models.Authentication.Update;

namespace TrainBookingSystem.API.Services.Authentication
{
    public interface IAuthService
    {
        Task<Response> RegisterAsync(RegisterUserDto registerUser, string role);
        Task<AuthResponse> LoginAsync(LoginDto loginDto);
        JwtSecurityToken GenerateJwtToken(IdentityUser user, IList<string> roles);
        Task<Response> ForgotPasswordAsync(string email);
        Task<Response> ResetPasswordAsync(ResetPasswordDto model);
        Task<Response> UpdateUserAsync(string userId, UpdateUserDto updateUserDto);
    }
}
