using Microsoft.EntityFrameworkCore;
using ToursimLoginAndRegistrationService.Interfaces;
using ToursimLoginAndRegistrationService.Models;

namespace ToursimLoginAndRegistrationService.Repositories
{
    public class TravellerRepository:IRepo<int, Traveller>
    {

        private readonly Context _context;
        private readonly ILogger<User> _logger;

        public TravellerRepository(Context context, ILogger<User> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<Traveller?> Add(Traveller item)
        {
            try
            {
                _context.Travellers.Add(item);
                await _context.SaveChangesAsync();
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<Traveller?> Delete(int key)
        {
            try
            {
                var traveller = await Get(key);
                if (traveller != null)
                {
                    _context.Travellers.Remove(traveller);
                    await _context.SaveChangesAsync();
                    return traveller;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<Traveller?> Get(int key)
        {
            try
            {
                var traveller = await _context.Travellers.Include(i => i.Users).FirstOrDefaultAsync(i => i.TravellerId == key);
                return traveller;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<ICollection<Traveller>?> GetAll()
        {
            try
            {
                var traveller = await _context.Travellers.ToListAsync();
                if (traveller.Count > 0)
                    return traveller;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<Traveller?> Update(Traveller item)
        {
            try
            {
                var traveller = _context.Travellers.FirstOrDefault(u => u.TravellerId == item.TravellerId); ;
                if (traveller != null)
                {

                    traveller.PhoneNumber = item.PhoneNumber != null ? item.PhoneNumber : traveller.PhoneNumber;

                    await _context.SaveChangesAsync();
                    return traveller;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }
    }
}
