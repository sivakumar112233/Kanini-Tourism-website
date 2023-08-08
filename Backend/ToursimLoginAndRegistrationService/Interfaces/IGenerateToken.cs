using ToursimLoginAndRegistrationService.Models.DTOs;

namespace ToursimLoginAndRegistrationService.Interfaces
{
    public interface IGenerateToken
    {
        public string GenerateToken(UserDTO user);
    }
}
