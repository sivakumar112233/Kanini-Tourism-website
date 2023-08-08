using Toursimtestimonyservice.Interfaces;
using Toursimtestimonyservice.Models;

namespace Toursimtestimonyservice.Services
{
    public class FeedBackRepository : IRepo<int, FeedBack>
    {
        private Context _context;

        public FeedBackRepository(Context context) { 
         _context=context;
        }
        public async  Task<FeedBack> Add(FeedBack item)
        {
            if (item != null)
            {
                _context.Add(item);
                _context.SaveChanges();
                return item;
            }
            return null;
        }

        public Task<FeedBack> Delete(int key)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<FeedBack>> GetAll()
        {
            return  _context.FeedBacks.ToList();
        }
    }
}
