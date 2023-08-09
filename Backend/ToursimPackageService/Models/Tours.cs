using System;
using System.ComponentModel.DataAnnotations;

namespace ToursimPackageService.Models
{
    public class Tours
    {
        [Key]
        public int TourId { get; set; }

        [Required(ErrorMessage = "Tour Name is required")]
        public string? TourName { get; set; }

        [Range(0.0, double.MaxValue, ErrorMessage = "Tour Price must be a positive value")]
        public float? TourPrice { get; set; }

        [StringLength(50, ErrorMessage = "Tour Location Country length cannot exceed 50 characters")]
        public string? TourLocationCountry { get; set; }

        [StringLength(50, ErrorMessage = "Tour Location State length cannot exceed 50 characters")]
        public string? TourLocationState { get; set; }

        [StringLength(50, ErrorMessage = "Tour Location City length cannot exceed 50 characters")]
        public string? TourLocationCity { get; set; }

        [StringLength(50, ErrorMessage = "Tour Specialty length cannot exceed 50 characters")]
        public string? TourSpecialty { get; set; }
        public Inclusion? Inclusion { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Max Count must be a positive value")]
        public int MaxCount { get; set; }

        [Required(ErrorMessage = "Travel Agent Id is required")]
        public int TravelAgentId { get; set; }
    }
}
