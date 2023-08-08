using ToursimBookingService.Models;
using ToursimBookingService.Models.DTOs;

namespace ToursimBookingService.Interfaces
{
    public interface IToursimBooking
    {
        public Task<Booking> BookingATour(Booking booking);
        public Task<Booking> CancellingATour(int bookingId);

        public Task<CountDTO>GetCountByTour(int TourId);
    }
}
