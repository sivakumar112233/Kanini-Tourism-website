using System.Security.Cryptography;
using System.Text;
using ToursimLoginAndRegistrationService.Interfaces;
using ToursimLoginAndRegistrationService.Models.DTOs;
using ToursimLoginAndRegistrationService.Models;
using ToursimLoginAndRegistrationService.Repositories;

namespace ToursimLoginAndRegistrationService.Services
{
    public class UserService:IUserService
    {
        private readonly IRepo<int, User> _userRepository;
        private readonly IRepo<int, Admin> _adminRepository;
        private readonly IRepo<int,Traveller> _travellerRepository;
        private readonly IRepo<int, TravelAgent> _travelagentRepository;

        private readonly IGenerateToken _tokenService;

        public UserService(
                                 IRepo<int,User> userRepository,
                                 IGenerateToken tokenService, IRepo<int, Admin> adminRepository, IRepo<int, Traveller> travellerRepository, IRepo<int,TravelAgent> travelagentRepository)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _adminRepository = adminRepository;
            _travellerRepository = travellerRepository;
            _travelagentRepository = travelagentRepository;
        }
        public async Task<UserDTO> Login(UserDTO userDTO)
        {
            UserDTO user = null;
            var users = await _userRepository.GetAll();
            var userData = users.FirstOrDefault(u => u.Email == userDTO.Email);
            if (userData != null)
            {
                var hmac = new HMACSHA512(userData.PasswordKey);
                var userPass = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDTO.Password));
                for (int i = 0; i < userPass.Length; i++)
                {
                    if (userPass[i] != userData.PasswordHash[i])
                        return null;
                }
                user = new UserDTO();
                user.UserId = userData.UserId;
                user.Role = userData.Role;
                if (user.Role != "travelagent")
                {
                    user.Token = _tokenService.GenerateToken(user);
                    return user;
                }
                var doctor = await _travelagentRepository.Get(user.UserId);
                if (doctor != null && doctor.IsActive == false)
                {
                    return user;
                }
                user.Token = (_tokenService.GenerateToken(user));
                return user;

            }
            return null;

        }

        public async Task<UserDTO> AdminRegistration(AdminDTO user)
        {
            UserDTO myUser = null;
            var hmac = new HMACSHA512();
            user.Users.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.PasswordClear));
            user.Users.PasswordKey = hmac.Key;
            user.Users.Role = "Admin";

            var users = await _adminRepository.GetAll();
            if (users != null)
            {
                var myAdminUser = users.FirstOrDefault(u => u.Email == user.Email && u.PhoneNumber == user.PhoneNumber);
                if (myAdminUser != null)
                {
                    return null;
                }
            }
            var userResult = await _userRepository.Add(user.Users);
            var adminResult = await _adminRepository.Add(user);
            if (userResult != null && adminResult != null)
            {
                myUser = new UserDTO();
                myUser.UserId = adminResult.AdminId;
                myUser.Role = userResult.Role;
                myUser.Token = _tokenService.GenerateToken(myUser);
            }
            return myUser;
        }

        public async Task<User?> GetByEmail(string email)
        {

            var users = await _userRepository.GetAll();

            var user = users.FirstOrDefault(u => u.Email == email);
            if (user != null)
            {
                return user;
            }
            else
            {
                throw new Exception("Database is empty");
            }
        }




    }
}
