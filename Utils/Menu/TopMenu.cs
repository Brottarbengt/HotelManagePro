using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagePro.Utils.Menu
{
    public class TopMenu
    {
        private static string[] menuOptions = new string[]
        {
            "New Booking",
            "Bookings Menu",
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
                    
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
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
