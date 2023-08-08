using System.Security.Cryptography;
using System.Text;
using ToursimLoginAndRegistrationService.Interfaces;
using ToursimLoginAndRegistrationService.Models.DTOs;
using ToursimLoginAndRegistrationService.Models;

namespace ToursimLoginAndRegistrationService.Services
{
    public class TravellerService:ITravellerService
    {
        private readonly IRepo<int,Traveller> _travellerRepo;
        private readonly IRepo<int,User> _userRepo;
        private readonly IGenerateToken _tokenService;
        private readonly IRepo<int,TravelAgent> _travelagentRepo;

        public TravellerService(IRepo<int, Traveller> travellerRepo, IRepo<int, User> userRepo, IGenerateToken tokenService, IRepo<int,TravelAgent> travelagentRepo)
        {

            _travellerRepo = travellerRepo;
            _userRepo = userRepo;
            _tokenService = tokenService;
            _travelagentRepo = travelagentRepo;
        }

        public async Task<UserDTO> TravellerRegistration(TravellerDTO user)
        {
            UserDTO myUser = null;
            var hmac = new HMACSHA512();
            user.Users.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.PasswordClear ?? "1234"));
            user.Users.PasswordKey = hmac.Key;
            user.Users.Role = "traveller";

            var users = await _travellerRepo.GetAll();
            if (users != null)
            {
                var myAdminUser = users.FirstOrDefault(u => u.Username == user.Username && u.PhoneNumber == user.PhoneNumber);
                if (myAdminUser != null)
                {
                    return null;
                }
            }
            var userResult = await _userRepo.Add(user.Users);
            var travellerResult = await _travellerRepo.Add(user);
            if (userResult != null && travellerResult != null)
            {
                myUser = new UserDTO();
                myUser.UserId = travellerResult.TravellerId;
                myUser.Role = userResult.Role;
                myUser.Token = _tokenService.GenerateToken(myUser);
            }
            return myUser;
        }




    }
}
