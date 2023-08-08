using ToursimLoginAndRegistrationService.Models.DTOs;

namespace ToursimLoginAndRegistrationService.Interfaces
{
    public interface IUserService
    {
        public Task<UserDTO?> Login(UserDTO user);
        public Task<UserDTO?> AdminRegistration(AdminDTO adminDTO);

    }
}
