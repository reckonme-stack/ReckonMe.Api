using System.ComponentModel.DataAnnotations;

namespace ReckonMe.Api.Dtos
{
    public class UserRegisterDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}