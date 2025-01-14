using HotelManagePro.Database;
using HotelManagePro.Features.Bookings.Models;
using HotelManagePro.Features.Rooms.Models;
using HotelManagePro.Utils;
using Microsoft.EntityFrameworkCore;

namespace HotelManagePro.Features.Bookings.Services
{
    public class BookingService
    {

        private readonly ApplicationDbContext _dbContext;

        public BookingService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public void CreateNewBooking(Booking newBooking)
        {
            _dbContext.Bookings.Add(newBooking);
            _dbContext.SaveChanges();
        }

        public void UpdateBooking(Booking booking)
        {
            _dbContext.Bookings.Update(booking);
            _dbContext.SaveChanges();
        }

        public List<Booking> GetAllBookings()
        {
            var bookings = _dbContext.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Rooms)
                .Include(b => b.Invoice)
                .ToList();  
            return bookings;
        }

        public void UpdateBooking()
        {
            throw new System.NotImplementedException();
        }

        public void RemoveBooking(int bookingId)
        {
            var booking = _dbContext.Bookings.FirstOrDefault(b => b.BookingId == bookingId);

            if (booking == null)
            {
                throw new ArgumentException($"Booking with ID {bookingId} could not be found.");
            }

            _dbContext.Bookings.Remove(booking);
            _dbContext.SaveChanges();
        }

        
                
        public List<Booking> FindActiveBookingByEmail(string customerEmail)
        {

            var dateTimeNow = DateOnly.FromDateTime(DateTime.Now);
            var bookings = _dbContext.Bookings
               .Include(b => b.Customer)
               .Include(b => b.Rooms)
               .Include(b => b.Invoice)
               .Where(b => b.Customer.Email.Contains(customerEmail) && b.ArrivalDate >= dateTimeNow)
               .ToList();

            return bookings;
        }

        public Booking? GetBookingById(int id)
        {
            return _dbContext.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Rooms)
                .FirstOrDefault(b => b.BookingId == id);
        }

        public List<DateOnly> GetRoomBookedDates(int roomNumber)
        {
            var bookedDates = _dbContext.Bookings
                .Where(b => b.Rooms.Any(r => r.RoomNumber == roomNumber) && b.ArrivalDate >= DateOnly.FromDateTime(DateTime.Now))
                .SelectMany(b => Enumerable.Range(0, b.DepartureDate.DayNumber - b.ArrivalDate.DayNumber)
                                           .Select(offset => b.ArrivalDate.AddDays(offset)))
                .ToList();

            return bookedDates;
        }
    }
}

