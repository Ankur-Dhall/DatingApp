using System.ComponentModel.DataAnnotations;
using System;

namespace Datingapp.API.Dtos
{
    public class UserToRegisterDto //Registering a new user.
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength(14, MinimumLength = 8, ErrorMessage = "Password length must be between 8-14 characters.")]
        public string Password { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string KnownAs { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Country { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }

        public UserToRegisterDto()
        {
            Created = DateTime.Now;
            LastActive = DateTime.Now;
        }
        
    }
}