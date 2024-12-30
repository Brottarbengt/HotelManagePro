using HotelManagePro.Utils.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagePro.Features.Customers.Menus
{
    public class CustomerMenu
    {
        private static string[] menuOptions = new string[] {
            
            "Search for Customer", // Undermeny med vad man vill söka på för Attrib? alt. söker igenom alla attribut på något som stämmer?
            "Show All Customers",
            "Remove Customers (soft Delete)",
            "DELETE Customer, (WARNING! Hard Delete, only if asked to.)", // Ska ShowAllCustomers
            "Edit Customer", // Ska ShowAllCustomers, Ska man kunna välja på samma sätt som menyn, dvs använda MenuGenerator?
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
                    ;
                    break;
                case 3:
                    ;
                    break;
                case 4:
                    ;
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
