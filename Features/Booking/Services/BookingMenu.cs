using HotelManagePro.Utils.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagePro.Features.Booking.Services
{
    namespace HotelManagePro.Features.Booking.Services
    {
        public static class BookingMenu
        {
            private static string[] menuOptions = new string[] {
            "New Booking",
            "Search for Booking",
            "Show All Bookings",
            "Remove Booking",
            "Edit Booking",
            "Back to Top Menu"
        };

            private static BookingService _bookingService; 
            public static void SetBookingService(BookingService bookingService)
            {
                _bookingService = bookingService;
            }

            public static void ShowMenu()
            {
                MenuGenerator.ShowMenu("Bookings Menu", menuOptions, ExecuteSelectedOption);
            }

            private static void ExecuteSelectedOption(int selectedIndex)
            {
                switch (selectedIndex)
                {
                    case 0:
                        _bookingService.CreateNewBooking();  
                        break;
                    case 1:
                        _bookingService.SearchBooking();    
                        break;
                    case 2:
                        _bookingService.ShowAllBookings(); 
                        break;
                    case 3:
                        _bookingService.ChooseBookingRemove();        
                        break;
                    case 4:
                        _bookingService.ChooseEditBooking();          
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
}
