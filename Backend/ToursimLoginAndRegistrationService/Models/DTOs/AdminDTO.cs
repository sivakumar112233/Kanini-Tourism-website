using ToursimLoginAndRegistrationService.Models;
namespace ToursimLoginAndRegistrationService.Models.DTOs
{
    public class AdminDTO :Admin
    {

        public AdminDTO()
        {
            Users = new User();
        }



        public string? PasswordClear { get; set; }

    }
}
