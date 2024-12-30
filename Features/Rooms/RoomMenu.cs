using HotelManagePro.Utils.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagePro.Features.Rooms
{

    // GOING TO BE REWORKED: When enter, allways show all rooms and then you can choose to edit a room or just quit.
    //                       Using spectre tables and same as datepicker but with rooms.
    //                       
                           
    public static class RoomMenu
    {
        private static string[] menuOptions = new string[] {
            "Manage rooms",           
            "Add room",
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
