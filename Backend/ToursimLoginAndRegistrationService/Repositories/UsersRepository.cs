using Microsoft.EntityFrameworkCore;
using ToursimLoginAndRegistrationService.Interfaces;
using ToursimLoginAndRegistrationService.Models;

namespace ToursimLoginAndRegistrationService.Repositories
{
    public class UsersRepository:IRepo<int,User>
    {
        private readonly Context _context;

        public UsersRepository(Context Context)
        {
            _context = Context;
        }
        public async Task<User?> Add(User user)
        {
            if (_context.Users != null)
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return user;
            }
            throw new Exception("Database is empty");
        }

        public async Task<User?> Delete(int key)
        {
            if (_context.Users != null)
            {
                var users = await Get(key);
                if (users != null)
                {
                    _context.Users.Remove(users);
                    await _context.SaveChangesAsync();
                    return users;
                }
                return null;
            }
            throw new Exception("Database is empty");
        }

        public async Task<User?> Get(int key)
        {
            if (_context.Users != null)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == key);
                return user;
            }

            throw new Exception("Database is empty");
        }

        public async Task<ICollection<User>?> GetAll()
        {
            if (_context.Users != null)
            {
                var users = await _context.Users.ToListAsync();
                return users;
            }
            throw new Exception("Database is empty");
        }

        public async Task<User?> Update(User user)
        {
            if (_context.Users != null)
            {
                var userDetails = await Get(user.UserId);
                if (userDetails != null)
                {
                    userDetails.Role = user.Role;
                    userDetails.PasswordKey = user.PasswordKey;
                    userDetails.PasswordHash = user.PasswordHash;
                    await _context.SaveChangesAsync();
                    return user;
                }
                return null;
            }
            throw new Exception("Database is empty");
        }
    }
}
