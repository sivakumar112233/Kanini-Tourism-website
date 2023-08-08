using ToursimPackageService.Models;
using ToursimPackageService.Models.DTOs;

namespace ToursimPackageService.Interfaces
{
    public interface IServices
    {
      
        public Task<ICollection<TourDetailsDTO?>> GetTourDetailsByTourName(string tourName);

        public Task<CountDTO?>GetCountOfToursBySpecailty(string specailty);

        public Task<Tours?>AddingNewTour(Tours tours);

        public Task<TourDetailsDTO?> GettingTourDetailsByTourId(int TourId);
      
        public Task<ICollection<TourNameDTO>> GetAllTourNameBySpeciality(string tourSpecialty);




    }
}
