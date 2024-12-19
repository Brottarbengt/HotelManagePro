using HotelManagePro.Display.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagePro.Features.Booking
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
            MenuGenerator.ShowMenu("Products Menu", menuOptions, ExecuteSelectedOption);
        }

        private static void ExecuteSelectedOption(int selectedIndex)
        {
            switch (selectedIndex)
            {
                case 0:
                    ;
                    break;
                case 1:
                    ;
                    break;
                case 2:
                    ;                    
                    break;
                case 3:
                    ;
                    break;
                case 4:
                    ;
                    break;
                case 5:
                    ;
                    break;
                case 6:
                    TopMenu.ShowMenu();
                    break;
                default:
                    break;
            }
        }
    }
}
