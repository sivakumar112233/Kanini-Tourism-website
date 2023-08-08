using Toursimtestimonyservice.Models;

namespace Toursimtestimonyservice.Interfaces
{
    public interface IFeedBackService
    {
        public Task<ICollection<FeedBack>>GetTopFiveFeedBacks();

        public Task<FeedBack>AddingNewFeedBack(FeedBack feedBack);  
    }
}
