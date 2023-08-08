using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ToursimBookingService.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options)
        {

        }
        

        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BookingUser> BookingUsers{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Booking>()
                .HasMany(b => b.BookingUsers)
                .WithOne(bu => bu.Booking)
                .HasForeignKey(bu => bu.BookingId)
                .OnDelete(DeleteBehavior.Cascade);

        }


    }
}
