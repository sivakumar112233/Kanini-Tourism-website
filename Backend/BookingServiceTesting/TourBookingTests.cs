using NUnit.Framework;
using Moq;
using ToursimBookingService.Interfaces;
using ToursimBookingService.Models;
using ToursimBookingService.Models.DTOs;
using ToursimBookingService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingServiceTesting
{
   
    public class TourBookingServiceTests
    {
        private Mock<IRepo<int, Booking>> _mockBookingRepository;
        private TourBookingService _bookingService;

        [SetUp]
        public void Setup()
        {
            _mockBookingRepository = new Mock<IRepo<int, Booking>>();
            _bookingService = new TourBookingService(_mockBookingRepository.Object);
        }

        [Test]
        public async Task BookingATourFailureTest()
        {
            // Arrange
            var existingBooking = new Booking
            {
                TravellerId = 1,
                BookingDate = DateTime.Now.Date,
                TourId = 2
            };
            var newBooking = new Booking
            {
                TravellerId = 1,
                BookingDate = DateTime.Now.Date,
                TourId = 2
            };

            var bookings = new List<Booking> { existingBooking };
            _mockBookingRepository.Setup(repo => repo.GetAll()).ReturnsAsync(bookings);

            // Act & Assert
            Assert.ThrowsAsync<Exception>(async () => await _bookingService.BookingATour(newBooking));
        }

        [Test]
        public async Task AddingANewBookinSuccessTest()
        {
            // Arrange
            var newBooking = new Booking
            {
                TravellerId = 1,
                BookingDate = DateTime.Now.Date,
                TourId = 3,
                NoOfPersons=1,
                BookingUsers = new List<BookingUser>
            {
                new BookingUser
                {
                    BookingUserName = "John Doe",
                    BookingUserGender = "Male",
                    BookingUserPhoneNumber = "1234567890",
                    BookingUserEmail = "user@example.com"
                }
            }
            };
             _mockBookingRepository.Setup(repo => repo.Add(It.IsAny<Booking>())).ReturnsAsync(newBooking);

            // Act
            var result = await _bookingService.BookingATour(newBooking);

            // Assert
            Assert.AreEqual(newBooking, result);
        }

        [Test]
        public async Task GettingTotalCountByTourIdTest()
        {
            // Arrange
            var tourId = 2;
            var bookings = new List<Booking>
            {
                new Booking { TourId = tourId, NoOfPersons = 5 },
                new Booking { TourId = tourId, NoOfPersons = 3 },
                new Booking { TourId = tourId, NoOfPersons = 2 }
            };
            _mockBookingRepository.Setup(repo => repo.GetAll()).ReturnsAsync(bookings);

            // Act
            var result = await _bookingService.GetCountByTour(tourId);

            // Assert
            Assert.AreEqual(10, result.BookingCount);
        }
    }
}
