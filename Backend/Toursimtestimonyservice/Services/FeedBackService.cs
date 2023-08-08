using Toursimtestimonyservice.Interfaces;
using Toursimtestimonyservice.Models;

namespace Toursimtestimonyservice.Services
{
    public class FeedBackService : IFeedBackService
    {
        private IRepo<int, FeedBack> _feedBackRepository;

        public FeedBackService( IRepo<int,FeedBack>feedBackRepository) { 

            _feedBackRepository=feedBackRepository;
        }
        public Task<FeedBack> AddingNewFeedBack(FeedBack feedBack)
        {
          var newFeedback=_feedBackRepository.Add(feedBack);
            if(newFeedback != null)
            {
                return newFeedback;
            }
            throw new Exception("unable to find");
        }

        public  async Task<ICollection<FeedBack>> GetTopFiveFeedBacks()
        {
            var feedBackCollection = await _feedBackRepository.GetAll();
            var topFeedBacks = feedBackCollection.Where(f => f.Rating > 4);
            if(topFeedBacks!=null)
            {
                return topFeedBacks.ToList();
            }
            throw new Exception("unable to find");


        }
    }
}
