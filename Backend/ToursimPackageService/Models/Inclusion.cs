using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using ToursimPackageService.Model;

namespace ToursimPackageService.Models
{
    public class Inclusion
    {
        [Key]
        public int InclusionId { get; set; }

        [Required(ErrorMessage = "Tour Id is required")]
        public int TourId { get; set; }

        [ForeignKey("TourId")]
        [JsonIgnore]
        public Tours? Tour { get; set; }

        [StringLength(100, ErrorMessage = "Accommodation length cannot exceed 100 characters")]
        public string? Accommodation { get; set; }

        [StringLength(100, ErrorMessage = "Meals length cannot exceed 100 characters")]
        public string? Meals { get; set; }

        [StringLength(100, ErrorMessage = "Transfer length cannot exceed 100 characters")]
        public string? Transfer { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Total Nights must be a non-negative value")]
        public int TotalNights { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Total Days must be a non-negative value")]
        public int TotalDays { get; set; }

        public ICollection<TotalDaysDescription>? TotalDaysDescriptions { get; set; }
    }
}
