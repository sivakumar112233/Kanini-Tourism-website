using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ToursimPackageService.Model;

namespace ToursimPackageService.Models.DTOs
{
    public class TourDetailsDTO

    {
        public int TourId { get; set; } 
        public string? TourName { get; set; }
        public float? TourPrice { get; set; }
        public string? TourLocationCountry { get; set; }
        public string? TourLocationState { get; set; }
        public Inclusion Inclusion { get; set; }
        public int MaxCount { get; set; }
       
   
    }
}
