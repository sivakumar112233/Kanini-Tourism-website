using System.ComponentModel.DataAnnotations;

namespace ToursimLoginAndRegistrationService.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }



        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string? Email { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordKey { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public string? Role { get; set; }

    }
}
