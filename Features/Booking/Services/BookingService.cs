

namespace HotelManagePro.Features.Booking.Services
{
    public class BookingService
    {
        public void CreateNewBooking()
        {
            Console.WriteLine("New Booking");
        }

        public void SearchBooking()
        {
            Console.WriteLine("Search for Booking");
        }

        public void ShowAllBookings()
        {
            Console.WriteLine("Show All Bookings");
        }

        public void RemoveBooking(int BookingId)
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

        public void EditBooking(int BookingId)
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
    }
}

