using Microsoft.AspNetCore.Cors;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToursimLoginAndRegistrationService.Models;
using ToursimLoginAndRegistrationService.Models.DTOs;
using ToursimLoginAndRegistrationService.Interfaces;
using ToursimLoginAndRegistrationService.Repositories;
using ToursimLoginAndRegistrationService.Services;

namespace ToursimLoginAndRegistrationService.Controllers
{
    [Route("api/[controller][Action]")]
    [ApiController]
    public class UserRegistrationController : ControllerBase
    {
        private IUserService _userService;
        private ITravelAgentService _travelAgentService;
        private IRepo<int, TravelAgent> _travelAgentRepository;
        private ITravellerService _travellerService;
        private IRepo<int, Traveller> _travellerRepository;

        public UserRegistrationController( IUserService userService ,ITravelAgentService travelAgentService,IRepo<int,TravelAgent>travelAgentRepository,ITravellerService travellerService, IRepo<int, Traveller> travellerRepository) {
        
         _userService= userService;
            _travelAgentService = travelAgentService;
            _travelAgentRepository = travelAgentRepository;
            _travellerService = travellerService;
            _travellerRepository=travellerRepository;

        }
        [HttpPost]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDTO>> Login(UserDTO userDTO)
        {
            var result = await _userService.Login(userDTO);
            if (result != null)
                return Ok(result);
            return BadRequest("Invalid credentials");

        }


    

        [HttpPost]
        [ProducesResponseType(typeof(AdminDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDTO>> RegisterAsAdmin(AdminDTO adminDTO)
        {
            try
            {
                var result = await _userService.AdminRegistration(adminDTO);
                if (result != null)
                    return Ok(result);
                else
                    return BadRequest("Unable to register at this moment");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDTO>> RegisterAsTravelAgent(TravelAgentDTO travelagentDTO)
        {
            try
            {
                var result = await _travelAgentService.TravelAgentRegistration(travelagentDTO);
                if (result != null)
                    return Ok(result);
                return BadRequest("Unable to register at this moment");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                // Handle or log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the request.");
            }

        }





        [HttpGet]
        [ProducesResponseType(typeof(List<TravelAgent>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ICollection<TravelAgent>>> GetAllTravelAgents()
        {
            try
            {
                var travelagents = await _travelAgentRepository.GetAll();
                if (travelagents != null)
                {
                    return Ok(travelagents);

                }
                return NotFound("No travelagents  available");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return null;

        }
        [HttpGet]
        [ProducesResponseType(typeof(TravelAgent), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TravelAgent>>GettingTravelAgentDetails(int index)
        {
            try
            {
                var travelAgents = await _travelAgentService.GetTravelAgentDetailsById(index);
                if (travelAgents != null)
                {
                    return Ok(travelAgents);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return null;
        }

        [HttpPost]
        [EnableCors("CORS")]

        [ProducesResponseType(typeof(TravelAgent), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TravelAgent>> UpdateTravelAgentStatus(StatusDTO statusDTO)
        {
            if (statusDTO != null)
            {
                var result = await _travelAgentService.TravelAgentStatusUpdate(statusDTO);
                if (result != null)
                {
                    return Ok(result);
                }
                return BadRequest("Cannot update TravelAgent status right now");
            }
            return BadRequest("Enter the credentials properly");
        }



        [HttpPost]
        [ProducesResponseType(typeof(TravelAgent), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TravelAgent>> UpdateTravelAgentDetails(TravelAgent travelagent)
        {
            if (travelagent != null)
            {
                var result = await _travelAgentRepository.Update(travelagent);
                if (result != null)
                {
                    return Ok(result);
                }
                return BadRequest("Cannot update TravelAgent Details right now");
            }
            return BadRequest("Enter the credentials properly");
        }
        [HttpPost]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<UserDTO>> RegisterAsTraveller(TravellerDTO travellerDTO)
        {
            var result = await _travellerService.TravellerRegistration(travellerDTO);
            if (result != null)
                return Ok(result);
            return BadRequest("Unable to register at this moment");
        }



        [HttpGet]

        [ProducesResponseType(typeof(List<Traveller>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ICollection<Traveller>>> GetAllTravellers()
        {
            try
            {
                var travellers = await _travellerRepository.GetAll();
                if (travellers != null)
                {
                    return Ok(travellers);

                }
                return NotFound("No travellers  available");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
    }
}




