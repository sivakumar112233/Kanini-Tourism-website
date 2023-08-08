using ToursimBookingService.Interfaces;
using ToursimBookingService.Models;
using ToursimBookingService.Models.DTOs;

namespace ToursimBookingService.Services
{
    public class TourBookingService : IToursimBooking
    {
        private IRepo<int, Booking> _bookingRepository;

        public TourBookingService(IRepo<int,Booking>bookingRepository) { 
          _bookingRepository=bookingRepository;
        }
        public async Task<Booking> BookingATour(Booking booking)
        {
            var bookings = await _bookingRepository.GetAll();

            var existingBooking = bookings.FirstOrDefault(b =>
                b.TravellerId == booking.TravellerId &&
                b.BookingDate.Date == booking.BookingDate.Date &&
                b.TourId == booking.TourId);

            if (existingBooking != null)
            {
                throw new Exception("Duplicate booking found.");
            }

            var newBooking = await _bookingRepository.Add(booking);
            if (newBooking != null)
            {
                return newBooking;
            }

            throw new Exception("Unable to add booking.");
        }

        public async Task<Booking> CancellingATour(int bookingId)
        {
            var bookings = await _bookingRepository.GetAll();
            var booking = bookings.FirstOrDefault(b => b.BookingId == bookingId);

            if (booking == null)
            {
                throw new Exception("Booking not found.");
            }

            if (booking.BookingDate >= DateTime.UtcNow)
            {
                throw new Exception("Cannot cancel a booking after the booking date.");
            }


            var cancelledBooking = await _bookingRepository.Delete(bookingId);
            if (cancelledBooking != null)
            {
                return cancelledBooking;
            }

            throw new Exception("Unable to cancel booking.");
        }

       
            public async Task<CountDTO> GetCountByTour(int tourId)
            {
                var bookings = await _bookingRepository.GetAll();

                var count = bookings
                    .Where(booking => booking.TourId == tourId)
                    .Sum(booking => booking.NoOfPersons);

                return new CountDTO { BookingCount = count };
            }

        
    }
}
