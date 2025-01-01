using HotelManagePro.Database;
using HotelManagePro.Features.Bookings.Models;
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
            _dbContext.Add(newBooking);
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

        

        public void RemoveBooking(int bookingId)
        {
            var booking = _dbContext.Bookings.FirstOrDefault(b => b.BookingId == bookingId);
            
            if (booking == null)
                throw new ArgumentException($"Bokning med ID {bookingId} hittades inte.");
        
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
    }
}

