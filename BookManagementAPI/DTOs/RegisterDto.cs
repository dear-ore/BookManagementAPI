using System.ComponentModel.DataAnnotations;

namespace BookManagementAPI.DTOs
{
    public class RegisterDto
    {
        [Required]
        [MaxLength(100)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }
    }
}
