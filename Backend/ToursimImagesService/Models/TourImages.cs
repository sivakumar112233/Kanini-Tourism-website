using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static ToursimImagesService.Models.TourImages;
using System.Text.Json.Serialization;

namespace ToursimImagesService.Models
{
    public class TourImages
    {
        [Key]
        public int ImageId { get; set; }
        public int TourId { get; set; }

    

        public   string? ImagePaths { get; set; }
        [NotMapped]
        public List<IFormFile> Images { get; set; }
       
    }
}
