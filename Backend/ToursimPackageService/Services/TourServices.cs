using System.Linq;
using System.Reflection.Metadata.Ecma335;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using ToursimPackageService.Interfaces;
using ToursimPackageService.Model;
using ToursimPackageService.Models;
using ToursimPackageService.Models.DTOs;
using static System.Net.Mime.MediaTypeNames;

namespace ToursimPackageService.Services
{
    public class TourServices:IServices
    {
      
        private IRepo<int, Tours> _tourRepository;
        private IRepo<int, Inclusion> _inclusionRepository;
        private IRepo<int, TotalDaysDescription> _totalDaysDetailsRepository;
       

        public TourServices(IRepo<int,Tours>tourRepository ,IRepo<int,Inclusion>inclusionRepository,
       
          IRepo<int,TotalDaysDescription>totalDaysDetailsRepository) { 

            _tourRepository=tourRepository;
            _inclusionRepository=inclusionRepository;
            _totalDaysDetailsRepository = totalDaysDetailsRepository;
     

        }



        public async Task<Tours?> AddingNewTour(Tours tours)
        {
            if (tours != null)
            {
                // Add the tour details to the database
                var addedTour = await _tourRepository.Add(tours);
                return addedTour;
            }

            throw new Exception("Unable to add at this moment");
        }

        public async Task<ICollection<TourNameDTO>> GetAllTourNameBySpeciality(string tourSpecialty)
        {
            if(tourSpecialty != null)
            {
                var allTours= await _tourRepository.GetAll();
                if (allTours.Count > 0)
                {
                    var tourNames=allTours.Where(a=>a.TourSpecialty == tourSpecialty).Select(a=>new TourNameDTO { TourName = a.TourName,TourId=a.TourId }).ToList();
                    return tourNames;
                }
            }
            throw new Exception("unable to find");
        }

        public async Task<CountDTO?> GetCountOfToursBySpecailty(string specailty)
        {
            if (specailty != null)
            {
                var tours = await _tourRepository.GetAll();
                var SpecailtyCount = tours.Where(t => t.TourSpecialty == specailty).Count();
                var Count = new CountDTO
                {
                    Count = SpecailtyCount
                };
                return Count;
                
            }
            throw new Exception("unable to get at this moment");
        }

        public  async Task<TourDetailsDTO> GettingTourDetailsByTourId(int tourId)
        {
            if (tourId>0)
            {
                var tours = await _tourRepository.GetAll();
                var particularTour = tours.FirstOrDefault(t => t.TourId == tourId);
                var tourInclusions = await _inclusionRepository.GetAll();
                var particularTourInclusions = tourInclusions.FirstOrDefault(i => i.InclusionId == particularTour.TourId);
                var totalDaysDescription = await _totalDaysDetailsRepository.GetAll();
                var particularTotalDaysDescription = totalDaysDescription
                                             .Where(t => t.InclusionId == particularTour.TourId)
                                             .Select(t => new TotalDaysDescription
                                             {
                                                 TourSpotName = t.TourSpotName,
                                                 DayDescription = t.DayDescription
                                             }).ToList();
                var tourDetailsDTO = new TourDetailsDTO
                {
                    TourId = particularTour.TourId,
                    TourName = particularTour.TourName,
                    TourLocationCountry = particularTour.TourLocationCountry,
                    TourLocationState = particularTour.TourLocationState,
                    Inclusion = particularTourInclusions,
                    TourPrice = particularTour.TourPrice,
                    MaxCount = particularTour.MaxCount

                };
                return tourDetailsDTO;

            }
            return null;

        }
    

        public async Task<ICollection<TourDetailsDTO?>> GetTourDetailsByTourName(string tourName)
        {
            if (tourName != null)
            {
                var tours = await _tourRepository.GetAll();
                var particularTours = tours.Where(t => t.TourName == tourName).ToList();

                if (particularTours.Any())
                {
                    var tourDetailsList = new List<TourDetailsDTO>();

                    foreach (var particularTour in particularTours)
                    {
                        var tourInclusions = await _inclusionRepository.GetAll();
                        var particularTourInclusions = tourInclusions.FirstOrDefault(i => i.InclusionId == particularTour.TourId);

                        var totalDaysDescription = await _totalDaysDetailsRepository.GetAll();
                        var particularTotalDaysDescription = totalDaysDescription
                               .Where(t => t.InclusionId == particularTour.TourId)
                               .Select(t => new TotalDaysDescription
                               {
                                   TourSpotName = t.TourSpotName,
                                   DayDescription = t.DayDescription
                               }).ToList();

                        var tourDetailsDTO = new TourDetailsDTO
                        {

                            TourId = particularTour.TourId,
                            TourName = particularTour.TourName,
                            TourLocationCountry = particularTour.TourLocationCountry,
                            TourLocationState = particularTour.TourLocationState,
                            TourPrice = particularTour.TourPrice,
                            Inclusion = particularTourInclusions,
                            MaxCount = particularTour.MaxCount
                         };

                        tourDetailsList.Add(tourDetailsDTO);
                    }

                    return tourDetailsList;
                }
            }

            throw new Exception("Unable to retrieve tour details at this moment");
        }
       
    }
}

