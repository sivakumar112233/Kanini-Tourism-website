using System.ComponentModel.DataAnnotations;

namespace Toursimtestimonyservice.Models
{
    public class FeedBack
    {
        [Key]
        public int FeedBackId { get; set; }

        [Required(ErrorMessage = "Feedback description is required.")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "Feedback description must be between 10 and 500 characters.")]
        public string FeedbackDescription { get; set; }

        [StringLength(100, ErrorMessage = "Tour name must not exceed 100 characters.")]
        public string TourName { get; set; }

        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int? Rating { get; set; }

        [Required(ErrorMessage = "User ID is required.")]
        public int? UserId { get; set; }
    }
}
