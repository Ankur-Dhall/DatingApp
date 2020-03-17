using System.ComponentModel.DataAnnotations;

namespace Datingapp.API.Dtos
{
    public class UserToRegisterDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength(14, MinimumLength = 8, ErrorMessage = "Password length must be between 8-14 characters.")]
        public string Password { get; set; }
        
    }
}