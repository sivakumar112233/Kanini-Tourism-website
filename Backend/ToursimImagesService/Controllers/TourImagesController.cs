// In your controller or business logic
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using ToursimImagesService.Interfaces;
using ToursimImagesService.Models;
using ToursimImagesService.Models.DTOs;

namespace ToursimImagesService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
 
    public class TourImagesController : ControllerBase
    {
        private readonly ITourImageService _tourImageService;
     

        public TourImagesController(ITourImageService tourImageService)
        {
            _tourImageService = tourImageService;
           
        }

        [HttpPost]
        public async Task<IActionResult> CreateTour( [FromForm] TourImages model ,List<IFormFile> images)
        {
            if (model != null && images != null && images.Count > 0)
            {
              

                await _tourImageService.UploadImagesAsync( model, images);

                return Ok("Tour created with images.");
            }

            return BadRequest("Invalid input data.");
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<ImageDTO>>>GettingAllImagesByTourId(int tourId)
        {
            if (tourId > 0)
            {
                var result = await _tourImageService.GettingAllImagesByTourId(tourId);
                return Ok(result);
            }

            return NotFound("not found");
        }
    }

}
