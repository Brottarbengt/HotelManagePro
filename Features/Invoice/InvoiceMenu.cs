using HotelManagePro.Utils.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagePro.Features.Invoice
{
    public class SearchBookingMenu
    {
        private static string[] menuOptions = new string[] {
            "Edit Invoice", // Shows all invoices first
            "Show All Invoices", // Showing all bookings with invoice or just invoices?
            "Edit Invoice", // Ska ShowAll() (en månad?) framöver ovan prompt
            "Back to Top Menu"
        };
        public static void ShowMenu()
        {
            MenuGenerator.ShowMenu("Invoice Menu", menuOptions, ExecuteSelectedOption);
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
                    TopMenu.ShowMenu();
                    break;
                default:
                    break;
            }
        }
    }
}
