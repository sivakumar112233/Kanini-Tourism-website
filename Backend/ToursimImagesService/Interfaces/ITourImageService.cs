using ToursimImagesService.Models;
using ToursimImagesService.Models.DTOs;

namespace ToursimImagesService.Interfaces
{
    public interface ITourImageService
    {
        public  Task UploadImagesAsync(TourImages tourImages, List<IFormFile> images);

        public Task<ICollection<ImageDTO>> GettingAllImagesByTourId(int tourId);
    }
}
