using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ToursimBookingService.Models
{
    public class BookingUser
    {
        [Key]
        public int BookingUserId { get; set; }

        public int BookingId { get; set; }
        [ForeignKey("BookingId")]
        [JsonIgnore]
        public Booking? Booking { get; set; }

        [Required(ErrorMessage = "BookingUserName is required.")]
        [StringLength(50, ErrorMessage = "BookingUserName cannot exceed 50 characters.")]
        public string? BookingUserName { get; set; }

        [Required(ErrorMessage = "BookingUserGender is required.")]
        [RegularExpression("^(Male|Female|Other)$", ErrorMessage = "BookingUserGender must be Male, Female, or Other.")]
        public string? BookingUserGender { get; set; }

        [Required(ErrorMessage = "BookingUserPhoneNumber is required.")]
        [RegularExpression("^[0-9]{10}$", ErrorMessage = "BookingUserPhoneNumber must be a 10-digit number.")]
        public string? BookingUserPhoneNumber { get; set; }

        [Required(ErrorMessage = "BookingUserEmail is required.")]
        [EmailAddress(ErrorMessage = "BookingUserEmail is not in a valid email format.")]
        public string? BookingUserEmail { get; set; }
    }
}
