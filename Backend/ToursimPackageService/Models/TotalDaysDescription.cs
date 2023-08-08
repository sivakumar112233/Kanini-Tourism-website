using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ToursimPackageService.Models;

namespace ToursimPackageService.Model
{
    public class TotalDaysDescription
    {
        [Key]
        public int TotalDaysDescriptionId { get; set; }
        public string ?TourSpotName { get; set; }
        public int InclusionId { get; set; }
        [ForeignKey("InclusionId")]
        [JsonIgnore]
        public Inclusion? Inclusion { get; set; }
      
        public string ? DayDescription { get; set; }




    }
}
