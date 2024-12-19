using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagePro.Display.Menu
{
    internal class TopMenu
    {
        private static string[] menuOptions = new string[] 
        { 
            "New Booking",
            "Boookings Menu",
            "Customer Menu", 
            "Room menu", 
            "Exit" 
        };

        public static void ShowMenu()
        {
            MenuGenerator.ShowMenu("Top Menu", menuOptions, ExecuteSelectedOption);
        }

        private static void ExecuteSelectedOption(int selectedIndex)
        {
            switch (selectedIndex)
            {
                case 0:
                    //NewBookingMenu.Showmenu();
                    break;
                case 1:
                    //EditBookingsMenu.ShowMenu();
                    break;
                case 2:
                    //EditCustomerDataMenu.ShowMenu();
                    break;
                case 3:
                    //EditRoomDataMenu.ShowMenu();
                    break;
                case 4:
                    Environment.Exit(0);
                    break;
                default:
                    break;
            }
        }
    }
}
