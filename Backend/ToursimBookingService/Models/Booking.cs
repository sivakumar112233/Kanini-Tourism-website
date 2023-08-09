using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToursimBookingService.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        [Required(ErrorMessage = "TravellerId is required.")]
        public int TravellerId { get; set; }

        [Required(ErrorMessage = "TourId is required.")]
        public int TourId { get; set; }

        [ Required(ErrorMessage = "NoOfPersons must be greater than 0.")]
        public int NoOfPersons { get; set; }

        [Required(ErrorMessage = "BookingDate is required.")]

        public DateTime BookingDate { get; set; }

        public ICollection<BookingUser>? BookingUsers { get; set; }
    }
}
