using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToursimBookingService.Interfaces;
using ToursimBookingService.Models;
using ToursimBookingService.Models.DTOs;

namespace ToursimBookingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("ReactCors")]
    public class ToursimBookingController : ControllerBase
    {
        private IToursimBooking _toursimBookingService;
        private readonly ILogger<ToursimBookingController> _logger;

        public ToursimBookingController(IToursimBooking toursimBookingService, ILogger<ToursimBookingController> logger) { 
          _toursimBookingService=toursimBookingService;
          _logger = logger;

        }   
        [HttpPost]
        [ProducesResponseType(typeof(Booking), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public  async Task<ActionResult<Booking>>AddingANewBooking(Booking booking)
        {
            try
            {
                if (booking != null)
                {
                    var result = await _toursimBookingService.BookingATour(booking);
                    if (result != null)
                        return Created("added", result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return BadRequest("unable to add");

        }
        [HttpDelete]
        [ProducesResponseType(typeof(Booking), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Booking>>CancellingABook(int bookingId)
        {
            try
            {
                if (bookingId>0)
                {
                    var cancelledBooking = await _toursimBookingService.CancellingATour(bookingId);
                    if (cancelledBooking != null)
                        return Ok(cancelledBooking);


                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return NotFound("not found");
        }
        [HttpGet]
        [ProducesResponseType(typeof(CountDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CountDTO>> GettingTotalCountByTourId(int tourId)
        {
            try
            {
                if (tourId > 0)
                {
                    var result = await _toursimBookingService.GetCountByTour(tourId);
                    if (result != null)
                        return Ok(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return NotFound("unable to get");
        }

    }
}
