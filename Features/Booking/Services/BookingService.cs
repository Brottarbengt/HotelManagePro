using HotelManagePro.Database;
using HotelManagePro.Features.Booking.Models;
using HotelManagePro.Utils;
using Microsoft.EntityFrameworkCore;

namespace HotelManagePro.Features.Booking.Services
{
    public class BookingService
    {

        private readonly ApplicationDbContext _dbContext;

        public BookingService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public void CreateNewBooking(Bookings newBooking)
        {
            _dbContext.Add(newBooking);
            _dbContext.SaveChanges();
        }

        public List<Bookings> GetAllBookings()
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
            var booking = _dbContext.Bookings.FirstOrDefault(b => b.BookingsId == bookingId);
            
            _dbContext.Bookings.Remove(booking);
            _dbContext.SaveChanges();
           
        }

        
                
        public List<Bookings> FindActiveBookingByEmail(string customerEmail)
        {

            var dateTimeNow = DateOnly.FromDateTime(DateTime.Now);
            var bookings = _dbContext.Bookings
               .Include(b => b.Customers)
               .Include(b => b.Rooms)
               .Include(b => b.Invoice)
               .Where(b => b.Customers.Email.Contains(customerEmail) && b.ArrivalDate >= dateTimeNow)
               .ToList();

            return bookings;
        }
    }
}

