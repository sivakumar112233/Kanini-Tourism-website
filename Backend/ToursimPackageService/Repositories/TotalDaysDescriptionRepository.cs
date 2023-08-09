using Microsoft.EntityFrameworkCore;
using ToursimPackageService.Interfaces;
using ToursimPackageService.Model;
using ToursimPackageService.Models;

namespace ToursimPackageService.Repositories
{
    public class TotalDaysDescriptionRepository : IRepo<int, TotalDaysDescription>
    {
        private readonly  Context _context;

        public TotalDaysDescriptionRepository(Context context) { 
            _context = context;
        
        }
        public async Task<TotalDaysDescription> Add(TotalDaysDescription item)
        {
            if (item != null)
            {
                _context.TotalDaysDescriptions.Add(item);
                _context.SaveChanges();
                return item;

            }
            return null;
        }

        public Task<TotalDaysDescription> Delete(int key)
        {
            throw new NotImplementedException();
        }

        public async  Task<TotalDaysDescription> Get(int key)
        {
            var totalDaysDescription = _context.TotalDaysDescriptions.FirstOrDefault(p => p.TotalDaysDescriptionId == key);
            if (totalDaysDescription != null)
            {
                return totalDaysDescription;
            }
            return null;
        }

        public async Task<ICollection<TotalDaysDescription>?> GetAll()
        {
           return await _context.TotalDaysDescriptions.ToListAsync();
        }

        public Task<TotalDaysDescription?> Update(TotalDaysDescription item)
        {
            throw new NotImplementedException();
        }
    }
}
