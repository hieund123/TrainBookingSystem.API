using System.ComponentModel.DataAnnotations;

namespace TrainBookingSystem.API.Models.Authentication.SignUp
{
    public class RegisterUserDto
    {
        [Required(ErrorMessage = "User Name is requied.")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Email is requied.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is requied.")]
        public string? Password { get; set; }
    }
}
