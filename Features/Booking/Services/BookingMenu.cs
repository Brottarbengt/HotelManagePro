using HotelManagePro.Utils.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagePro.Features.Booking.Services
{
    public static class BookingMenu
    {

        private static string[] menuOptions = new string[] {
            "New Booking",
            "Search for Booking", // Undermeny med vad man vill söka på för Attrib? alt. söker igenom alla attribut på något som stämmer?
            "Show All Bookings",
            "Remove Booking", // Ska ShowAllBookings en månad framöver ovan prompt
            "Edit Booking", // Ska ShowAllBookings en månad framöver ovan prompt
            "Back to Top Menu"
        };

        public static void ShowMenu()
        {
            MenuGenerator.ShowMenu("Bookings Menu", menuOptions, ExecuteSelectedOption);
        }

        private static void ExecuteSelectedOption(int selectedIndex)
        {
            switch (selectedIndex)
            {
                case 0:
                    BookingService.CreateNewBooking();
                    break;
                case 1:
                    BookingService.SearchBooking();
                    break;
                case 2:
                    BookingService.ShowAllBookings();
                    break;
                case 3:
                    BookingService.RemoveBooking();
                    break;
                case 4:
                    BookingService.EditBooking();
                    break;
                case 5:
                    TopMenu.ShowMenu();
                    break;
                default:
                    break;
            }
        }
    }
}
