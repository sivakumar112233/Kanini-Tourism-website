using Microsoft.EntityFrameworkCore;
using ToursimBookingService.Interfaces;
using ToursimBookingService.Models;

namespace ToursimBookingService.Repositories
{
    public class BookingRepository : IRepo<int, Booking>
    {
        private Context _context;

        public BookingRepository(Context context)
        {
            _context = context;
        }

        public async Task<Booking> Add(Booking item)
        {
            _context.Bookings.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Booking> Delete(int key)
        {
            var DeletedBooking = await _context.Bookings.FindAsync(key);

            if (DeletedBooking == null)
            {
                return null;
            }

            _context.Bookings.Remove(DeletedBooking);

            await _context.SaveChangesAsync();

            return DeletedBooking;
        }



        public async Task<Booking> Get(int key)
        {
            var booking = await _context.Bookings.FindAsync(key);
            if (booking != null)
                return booking;
            return booking;
        }


        public async Task<ICollection<Booking>> GetAll()
        {
            return await _context.Bookings.ToListAsync();
        }


    }
}




