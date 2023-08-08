using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using ToursimPackageService.Model;
using ToursimPackageService.Models;

namespace ToursimPackageService.Models
{
    public class Inclusion
    {
        [Key]
        public int InclusionId { get; set; }

        public int TourId { get; set; }
        [ForeignKey("TourId")]

      
        public string? Accommodation { get; set; }
        public string? Meals { get; set; }
        public string? Transfer { get; set; }
        public int TotalNights { get; set; }
        public int TotalDays { get; set; }  
        public ICollection< TotalDaysDescription>? TotalDaysDescriptions { get; set; }
     

    }
}
