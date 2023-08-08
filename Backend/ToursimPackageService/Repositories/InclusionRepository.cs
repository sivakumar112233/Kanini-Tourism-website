using ToursimPackageService.Interfaces;
using ToursimPackageService.Models;

namespace ToursimPackageService.Repositories
{
    public class InclusionRepository : IRepo<int, Inclusion>
    {
        private readonly Context _context;

        public InclusionRepository(Context context) {
            _context=context;
        
        }
        public async Task<Inclusion> Add(Inclusion item)
        {
            if(item != null) { 
                 _context.Inclusion.Add(item);
                _context.SaveChanges();
                return item;

            }
            return null;
        }

        public Task<Inclusion?> Delete(int key)
        {
            var inclusion = Get(key);
            if (inclusion != null)
            {
                _context.Remove(inclusion);
                _context.SaveChanges();
                return inclusion;
            }
            return null;
        }

        public async  Task<Inclusion?> Get(int key)
        {
            var inclusion = _context.Inclusion.FirstOrDefault(p => p.InclusionId == key);
            if (inclusion != null)
            {
                return inclusion;
            }
            return null;
        }

        public async Task<ICollection<Inclusion>?> GetAll()
        {
            var inclusions = _context.Inclusion.ToList();
            if (inclusions.Count > 0)
            {
                return inclusions;
            }
            return null;
        }

        public Task<Inclusion?> Update(Inclusion item)
        {
            throw new NotImplementedException();
        }
    }
}
