using System.ComponentModel.DataAnnotations;

namespace TrainBookingSystem.API.Models.Authentication.Password
{
    public class ForgotPasswordDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
