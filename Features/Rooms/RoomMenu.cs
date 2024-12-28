using HotelManagePro.Utils.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagePro.Features.Rooms
{
    public static class RoomMenu
    {
        private static string[] menuOptions = new string[] {
            "Manage Rooms",    // shows all rooms first        
            "Show All Rooms",
            "Back to Top Menu"
        };
        public static void ShowMenu()
        {
            MenuGenerator.ShowMenu("Customer Menu", menuOptions, ExecuteSelectedOption);
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
                    TopMenu.ShowMenu();
                    break;
                default:
                    break;
            }

        }
    }
}
