using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ToursimPackageService.Models
{
    public class Tours
    {
        [Key]
        public int TourId { get; set; }
      
       public string ?TourName { get; set; }
      
        public float ?TourPrice{ get; set; }
        public string? TourLocationCountry { get; set; }
        public string  ?TourLocationState{get;set;}
        //public int InclusionId { get; set; }
        //[JsonIgnore]
        //public Inclusion Inclusion { get; set; }    
        public string ? TourLocationCity { get; set; }
        public string? TourSpecialty { get; set; }
        public Inclusion Inclusion { get; set; }

        public int MaxCount { get; set; }
        public int TravelAgentId { get; set; }
       

    }
}
