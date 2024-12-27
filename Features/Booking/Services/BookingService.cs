using HotelManagePro.Database;
using HotelManagePro.Features.Booking.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagePro.Features.Booking.Services
{
    public class BookingService
    {

        // Behöver refactor ChooseBookingIdRemove/Edit to one method
        private readonly ApplicationDbContext _dbContext;

        public BookingService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void CreateNewBooking()
        {
            Console.WriteLine("Create New Booking:");

            Console.WriteLine("Enter Customer ID:");
            if (!int.TryParse(Console.ReadLine(), out int customerId))
            {
                Console.WriteLine("Invalid Customer ID.");
                return;
            }

            var customer = _dbContext.Customers.Find(customerId);
            if (customer == null)
            {
                Console.WriteLine("Customer not found.");
                return;
            }

            Console.WriteLine("Enter Arrival Date (yyyy-MM-dd):");
            if (!DateOnly.TryParse(Console.ReadLine(), out DateOnly arrivalDate))
            {
                Console.WriteLine("Invalid Arrival Date.");
                return;
            }

            Console.WriteLine("Enter Departure Date (yyyy-MM-dd):");
            if (!DateOnly.TryParse(Console.ReadLine(), out DateOnly departureDate) || departureDate <= arrivalDate)
            {
                Console.WriteLine("Invalid or conflicting Departure Date.");
                return;
            }

            Console.WriteLine("Enter Room IDs (comma-separated):");
            string[] roomIdsInput = Console.ReadLine()?.Split(',') ?? Array.Empty<string>();
            var roomIds = roomIdsInput
                .Where(id => int.TryParse(id, out _))
                .Select(int.Parse)
                .ToList();

            var rooms = _dbContext.Rooms.Where(r => roomIds.Contains(r.RoomsId)).ToList();
            if (rooms.Count != roomIds.Count)
            {
                Console.WriteLine("Some rooms were not found.");
                return;
            }

            var booking = new Bookings
            {
                Customer = customer,
                ArrivalDate = arrivalDate,
                DepartureDate = departureDate,
                Rooms = rooms
            };

            _dbContext.Bookings.Add(booking);
            _dbContext.SaveChanges();
            Console.WriteLine("Booking successfully created.");
        }

        public void SearchBooking()
        {
            Console.WriteLine("Search for Booking");
        }

        public void ShowAllBookings()
        {
            var bookings = _dbContext.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Rooms)
                .Include(b => b.Invoice)
                .ToList();

            if (bookings.Count == 0)
            {
                Console.WriteLine("No bookings available.");
                return;
            }

            foreach (var booking in bookings)
            {
                DisplayBooking(booking);
            }
        }

        public void ChooseBookingRemove()
        {
            ShowAllBookings();
            Console.WriteLine("Enter Booking ID to remove:");
            if (int.TryParse(Console.ReadLine(), out int bookingId))
            {
                RemoveBooking(bookingId);
            }
            else
            {
                Console.WriteLine("Invalid input, returning to menu.");  // Sånt här kanske hamnar i validation senare
            };
        }

        public void RemoveBooking(int bookingId)
        {
            var booking = _dbContext.Bookings.FirstOrDefault(b => b.BookingsId == bookingId);
            if (booking == null)
            {
                Console.WriteLine("Booking not found.");
                return;
            }

            _dbContext.Bookings.Remove(booking);
            _dbContext.SaveChanges();
            Console.WriteLine("Booking successfully removed.");
        }

        public void ChooseEditBooking()
        {
            ShowAllBookings();
            Console.WriteLine("Enter Booking ID to edit:");
            if (int.TryParse(Console.ReadLine(), out int bookingId))
            {
                EditBooking(bookingId);
            }
            else
            {
                Console.WriteLine("Invalid input, returning to menu.");
            }
        }

        public void EditBooking(int bookingId)
        {
            var booking = _dbContext.Bookings
                .Include(b => b.Rooms)
                .FirstOrDefault(b => b.BookingsId == bookingId);

            if (booking == null)
            {
                Console.WriteLine("Booking not found.");
                return;
            }

            Console.WriteLine($"Current Arrival Date: {booking.ArrivalDate}. Enter new Arrival Date (yyyy-MM-dd):");
            if (DateOnly.TryParse(Console.ReadLine(), out DateOnly newArrivalDate))
            {
                booking.ArrivalDate = newArrivalDate;
            }
            else
            {
                Console.WriteLine("Invalid date. Keeping original arrival date.");
            }

            Console.WriteLine($"Current Departure Date: {booking.DepartureDate}. Enter new Departure Date (yyyy-MM-dd):");
            if (DateOnly.TryParse(Console.ReadLine(), out DateOnly newDepartureDate))
            {
                booking.DepartureDate = newDepartureDate;
            }
            else
            {
                Console.WriteLine("Invalid date. Keeping original departure date.");
            }

            _dbContext.SaveChanges();
            Console.WriteLine("Booking successfully updated.");
        }
        public void SearchByCustomerName(string customerName)
        {
            var bookings = _dbContext.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Rooms)
                .Include(b => b.Invoice)
                .Where(b => b.Customer.FirstName.Contains(customerName) || b.Customer.LastName.Contains(customerName))
                .ToList();

            if (!bookings.Any())
            {
                Console.WriteLine("No bookings found for the specified customer name.");
                return;
            }

            foreach (var booking in bookings)
            {
                DisplayBooking(booking);
            }
        }

        public void SearchByRoomNumber(int roomNumber)
        {
            var bookings = _dbContext.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Rooms)
                .Include(b => b.Invoice)
                .Where(b => b.Rooms.Any(r => r.RoomNumber == roomNumber))
                .ToList();

            if (!bookings.Any())
            {
                Console.WriteLine("No bookings found for the specified room number.");
                return;
            }

            foreach (var booking in bookings)
            {
                DisplayBooking(booking);
            }
        }

        public void DisplayBooking(Bookings booking)
        {
            Console.WriteLine($"Booking ID: {booking.BookingsId}");
            Console.WriteLine($"Customer: {booking.Customer.FirstName} {booking.Customer.LastName}");
            Console.WriteLine($"Arrival Date: {booking.ArrivalDate}");
            Console.WriteLine($"Departure Date: {booking.DepartureDate}");
            Console.WriteLine($"Rooms: {string.Join(", ", booking.Rooms.Select(r => $"Room {r.RoomNumber} ({r.RoomType})"))}");
            Console.WriteLine($"Is Paid: {booking.Invoice?.IsPaid ?? false}");
            Console.WriteLine($"Total Cost: {booking.Invoice?.TotalSum ?? 0}");
            Console.WriteLine(new string('-', 40));
        }
        
    }
}

