using ToursimLoginAndRegistrationService.Models;
using ToursimLoginAndRegistrationService.Models.DTOs;

namespace ToursimLoginAndRegistrationService.Interfaces
{
    public interface ITravelAgentService
    {
        public Task<UserDTO?> TravelAgentRegistration(TravelAgentDTO travelAgentDTO);


        public Task<StatusDTO> TravelAgentStatusUpdate(StatusDTO status);

        public Task<TravelAgent> GetTravelAgentDetailsById(int travelAgentId);


    }
}
