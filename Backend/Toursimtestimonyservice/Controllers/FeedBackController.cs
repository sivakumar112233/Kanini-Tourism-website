using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Toursimtestimonyservice.Interfaces;
using Toursimtestimonyservice.Models;

namespace Toursimtestimonyservice.Controllers
{
    [Route("api/[controller][Action]")]
    [ApiController]
    public class FeedBackController : ControllerBase
    {
        private IFeedBackService _feedBackService;
        private ILogger<FeedBackController> _logger;

        public FeedBackController(IFeedBackService feedBackService, ILogger<FeedBackController> logger) { 
            _feedBackService= feedBackService;
            _logger= logger;
        
        }


        [HttpPost]
        [ProducesResponseType(typeof(FeedBack), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<FeedBack>> AddingANewFeedBack(FeedBack feedBack)
        {
            try
            {
                if (feedBack != null)
                {
                    var result = await _feedBackService.AddingNewFeedBack(feedBack);
                    if (result != null)
                    {
                        return Created("created", result);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return BadRequest("unable to add");
        }

        [HttpGet]
        [ProducesResponseType(typeof(ICollection<FeedBack>),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ICollection<FeedBack>>> GettingTopFeedBacks()
        {
            try
            {
                var topFeedbacks = await _feedBackService.GetTopFiveFeedBacks();
                if (topFeedbacks != null)
                {
                    return Ok(topFeedbacks);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return NotFound("no feedbacks");
        }
    }
}
