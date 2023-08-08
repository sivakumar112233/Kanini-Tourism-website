
using ToursimImagesService.Models;
using ToursimImagesService.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace ToursimImagesService.Respositories
{

    public class TourImagesRepository : IRepo<int, TourImages>
        {
            private readonly TourImageContext _context;

            public TourImagesRepository(TourImageContext context)
            {
                _context = context;
            }

        public async Task<TourImages?> Add(TourImages item)
        { 
            if(item!=null)
            {
                _context.TourPackageImagesContainer.Add(item);
               await _context.SaveChangesAsync();    
                return item;

            }
            return null;
            
        }

      
        public async  Task<TourImages?> Get(int key)
        {
            var tour = await _context.TourPackageImagesContainer.FindAsync(key);
            if(tour!=null)
            {
                return tour;
            }
            return null;
        }

        public  async Task<ICollection<TourImages?>> GetAll()
        {
          return  await  _context.TourPackageImagesContainer.ToListAsync();
            
        }
    }
}
