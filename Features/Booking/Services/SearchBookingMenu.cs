using HotelManagePro.Utils.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagePro.Features.Booking.Services
{
    public class SearchBookingMenu
    {
        private readonly BookingService _bookingService;

        public SearchBookingMenu(BookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public void ShowMenu()
        {
            string[] options = new[]
            {
                "Search by Customer Name",
                "Search by Room Number",
                "Back to Booking Menu"
            };

            MenuGenerator.ShowMenu("Search Bookings", options, ExecuteOption);
        }

        private void ExecuteOption(int selectedIndex)
        {
            switch (selectedIndex)
            {
                case 0:
                    SearchByCustomerName();
                    break;
                case 1:
                    SearchByRoomNumber();
                    break;
                case 2:
                    return; // Exit to previous menu
            }

            Console.WriteLine("Press any key to return to the menu...");
            Console.ReadKey();
        }

        private void SearchByCustomerName()
        {
            Console.WriteLine("Enter Customer Name:");
            string customerName = Console.ReadLine();
            _bookingService.SearchByCustomerName(customerName);
        }

        private void SearchByRoomNumber()
        {
            Console.WriteLine("Enter Room Number:");
            if (int.TryParse(Console.ReadLine(), out int roomNumber))
            {
                _bookingService.SearchByRoomNumber(roomNumber);
            }
            else
            {
                Console.WriteLine("Invalid room number.");
            }
        }
    }
}
