using System.Security.Cryptography;
using System.Text;
using ToursimLoginAndRegistrationService.Interfaces;
using ToursimLoginAndRegistrationService.Models.DTOs;
using ToursimLoginAndRegistrationService.Models;

namespace ToursimLoginAndRegistrationService.Services
{
    public class TravelAgentService:ITravelAgentService
    {
        private readonly IRepo<int,TravelAgent> _travelagentRepository;
        private readonly IRepo<int,User> _userRepository;
        private readonly IGenerateToken _tokenService;

        public TravelAgentService(IRepo<int,TravelAgent> travelagentRepository, IRepo<int, User> userRepository, IGenerateToken tokenService)
        {

            _travelagentRepository = travelagentRepository;
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public Task<TravelAgent> GetTravelAgentDetailsById(int travelAgentId)
        {
            if(travelAgentId > 0)
            {
                var travelAgent=_travelagentRepository.Get(travelAgentId);
                if(travelAgent != null)
                {
                    return travelAgent;
                }
            }
            throw new Exception("unable to find");
        }

        public async Task<UserDTO> TravelAgentRegistration(TravelAgentDTO user)
        {
            UserDTO myUser = null;
            var hmac = new HMACSHA512();
            user.Users.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.PasswordClear));
            user.Users.PasswordKey = hmac.Key;
            user.Users.Role = "TravelAgent";
            user.IsActive = false;

            var users = await _travelagentRepository.GetAll();
            if (users != null)
            {
                var myAdminUser = users.FirstOrDefault(u => u.ContactNumber == user.ContactNumber && u.Username == user.Username);
                if (myAdminUser != null)
                {
                    return null;
                }
            }
            var userResult = await _userRepository.Add(user.Users);
            var travelagentResult = await _travelagentRepository.Add(user);
            if (userResult != null && travelagentResult != null)
            {
                myUser = new UserDTO();
                myUser.UserId = travelagentResult.TravelAgentId;
                myUser.Role = userResult.Role;
                myUser.Token = _tokenService.GenerateToken(myUser);
            }
            return myUser;
        }

        public async Task<StatusDTO> TravelAgentStatusUpdate(StatusDTO status)
        {
            var travelagent = await _travelagentRepository.Get(status.TravelAgentId);
            if (travelagent != null)
            {
                travelagent.IsActive = status.IsActive;
                var updateTravelAgent = await _travelagentRepository.Update(travelagent);
                if (updateTravelAgent != null)
                    return status;
                return null;
            }
            return null;
        }



    }
}
