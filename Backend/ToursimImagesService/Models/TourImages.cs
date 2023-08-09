using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ToursimImagesService.Models
{
    public class TourImages
    {
        [Key]
        public int ImageId { get; set; }

        [Required(ErrorMessage = "Tour ID is required.")]
        public int TourId { get; set; }

       
        public string ?ImagePaths { get; set; }

        [NotMapped]
       
        public List<IFormFile> ?Images { get; set; }
    }

    
    }

