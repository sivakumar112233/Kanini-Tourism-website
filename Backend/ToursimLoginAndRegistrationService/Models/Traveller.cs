using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ToursimLoginAndRegistrationService.Models
{
    public class Traveller
    {

        [Key]
        public int TravellerId { get; set; }
        [ForeignKey("TravellerId")]
        public User? Users { get; set; }
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, ErrorMessage = "Username cannot exceed 50 characters.")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Contact Number is required.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid contact number format. Use a 10-digit number.")]
        public string? PhoneNumber { get; set; }

        public string? Gender { get; set; }

    }
}
