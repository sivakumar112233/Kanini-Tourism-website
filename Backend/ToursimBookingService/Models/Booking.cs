using System.ComponentModel.DataAnnotations;

namespace ToursimBookingService.Models
{
    public class Booking
    {
     [Key]
     public int BookingId { get; set; } 
     public int TravellerId { get; set; }   

    public int TourId { get; set; }

    public int NoOfPersons { get; set; }
     
    public DateTime BookingDate { get; set; }
    public ICollection<BookingUser>? BookingUsers { get; set; }
    }
}
