using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToursimPackageService.Interfaces;
using ToursimPackageService.Model;
using ToursimPackageService.Models;
using ToursimPackageService.Models.DTOs;
using ToursimPackageService.Services;

namespace ToursimPackageService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TourController : ControllerBase
    {
        private readonly IServices _tourServices;
        private readonly ILogger<TourController> _logger;


        public TourController(IServices tourServices, ILogger<TourController> logger)
        {

            _tourServices = tourServices;
            _logger = logger;
        }
        [HttpPost]
        [ProducesResponseType(typeof(Tours), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Tours>> AddingANewTourPackage(Tours tours)
        {
            try
            {
                if (tours != null)
                {
                    var result = await _tourServices.AddingNewTour(tours);

                    return Created("tourPackages created", result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return BadRequest("Unable to register");
        }


        [HttpGet]
        [ProducesResponseType(typeof(TourDetailsDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<TourDetailsDTO>> GetAllTourDetailsByTourName(string tourName)
        {
            try
            {
                if (!string.IsNullOrEmpty(tourName))
                {
                    var tourDetails = await _tourServices.GetTourDetailsByTourName(tourName);
                    return Ok(tourDetails);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return BadRequest("unable to find");


        }
        [HttpGet]
        [ProducesResponseType(typeof(CountDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]


        public async Task<ActionResult<CountDTO>> GettingCountByTourSpecailty(string tourSpecailty)
        {
            try
            {
                if (string.IsNullOrEmpty(tourSpecailty))
                {
                    var Count = await _tourServices.GetCountOfToursBySpecailty(tourSpecailty);
                    return Ok(Count);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return BadRequest("unable to fetch");
        }

        [HttpGet]
        [ProducesResponseType(typeof(TourDetailsDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Tours>> GettingATourByTourId(int tourId)
        {
            try
            {
                if (tourId > 0)
                {
                    var tour = await _tourServices.GettingTourDetailsByTourId(tourId);
                    return Ok(tour);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return BadRequest("unable to find");

        }
        [HttpGet]
        [ProducesResponseType(typeof(ICollection<TourNameDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ICollection<TourNameDTO>>>GettingAllTourNamesBySpeciality(string tourSpecialty)
         {
            try
            {
                if (tourSpecialty != null)
                {
                    var result = await _tourServices.GetAllTourNameBySpeciality(tourSpecialty);
                    if (result != null)
                        return Ok(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return BadRequest("unable to fetch");
        }

        [HttpGet]
        public async Task<ActionResult<List<TotalDaysDescription>>> getAll()
        {
            var objects =  await _tourServices.GetAllDescriptions();
            return Ok(objects);
        }
    }
}