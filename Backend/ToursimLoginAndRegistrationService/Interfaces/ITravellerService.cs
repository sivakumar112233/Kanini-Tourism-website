using ToursimLoginAndRegistrationService.Models.DTOs;

namespace ToursimLoginAndRegistrationService.Interfaces
{
    public interface ITravellerService
    {
        public Task<UserDTO?> TravellerRegistration(TravellerDTO travellerDTO);
    }
}
