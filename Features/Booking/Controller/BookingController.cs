using HotelManagePro.Database;
using HotelManagePro.Features.Booking.Models;
using HotelManagePro.Features.Invoice.Models;
using HotelManagePro.Features.Room.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagePro.Features.Booking.Controller
{
    public class BookingController
    {
        private readonly ApplicationDbContext _dbContext;

        public BookingController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool IsRoomAvailable(int roomId, DateOnly arrivalDate, DateOnly departureDate)
        {
            return !_dbContext.Bookings
                .Include(b => b.Rooms)
                .Any(b => b.Rooms.Any(r => r.RoomsId == roomId) &&
                          ((arrivalDate >= b.ArrivalDate && arrivalDate < b.DepartureDate) ||
                           (departureDate > b.ArrivalDate && departureDate <= b.DepartureDate)));
        }

        
        public Bookings CreateBooking(int customerId, DateOnly arrivalDate, DateOnly departureDate, List<int> roomIds)
        {
            // Validate customer exists
            var customer = _dbContext.Customers.Include(c => c.Bookings).FirstOrDefault(c => c.CustomersId == customerId);
            if (customer == null)
                throw new ArgumentException("Customer does not exist.");

            // Validate rooms
            var rooms = _dbContext.Rooms.Where(r => roomIds.Contains(r.RoomsId)).ToList();
            if (rooms.Count != roomIds.Count)
                throw new ArgumentException("One or more rooms do not exist.");
            if (rooms.Any(r => !IsRoomAvailable(r.RoomsId, arrivalDate, departureDate)))
                throw new ArgumentException("One or more rooms are unavailable for the selected dates.");

            // Create invoice
            var invoice = new Invoices
            {
                TotalSum = CalculateTotalPrice(rooms, arrivalDate, departureDate),
                IsPaid = false
            };
            _dbContext.Invoices.Add(invoice);

            // Create booking
            var booking = new Bookings
            {
                Invoice = invoice,
                ArrivalDate = arrivalDate,
                DepartureDate = departureDate,
                Rooms = rooms
            };
            customer.Bookings.Add(booking);
            _dbContext.Bookings.Add(booking);

            _dbContext.SaveChanges();
            return booking;
        }

        
        private int CalculateTotalPrice(List<Rooms> rooms, DateOnly arrivalDate, DateOnly departureDate)
        {
            var days = (departureDate.ToDateTime(TimeOnly.MinValue) - arrivalDate.ToDateTime(TimeOnly.MinValue)).Days;
            var pricePerDay = rooms.Sum(r => r.RoomType == TypeOfRoom.Single ? 500 : 1000); // Magic Strings! Måste ordna detta OBS OBS OBS!!!
            return pricePerDay * days;
        }
    }
}
