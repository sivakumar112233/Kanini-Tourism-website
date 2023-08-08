using ToursimPackageService.Interfaces;
using ToursimPackageService.Models;

namespace ToursimPackageService.Repositories
{
    public class ToursRepository : IRepo<int, Tours>
    {
        private readonly Context _context;

        public ToursRepository(Context context) { 
            _context = context;
        
        }
        public async Task<Tours> Add(Tours item)
        {
            if (item != null)
            {
                _context.Tours.Add(item);
                _context.SaveChanges();
                return item;

            }
            return null;
        }

        public Task<Tours> Delete(int key)
        {
            throw new NotImplementedException();
        }

        public async Task<Tours> Get(int key)
        {
            var tours = await _context.Tours.FindAsync(key); 
            if (tours != null)
            {
                return tours;
            }
            return null;
        }

        public async Task<ICollection<Tours>?> GetAll()
        {
            var tours = _context.Tours.ToList();
            if (tours.Count > 0)
            {
                return tours;
            }
            return null;
        }

        public Task<Tours> Update(Tours item)
        {
            throw new NotImplementedException();
        }
    }
}
