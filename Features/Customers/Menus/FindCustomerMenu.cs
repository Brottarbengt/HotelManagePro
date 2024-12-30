using HotelManagePro.Features.Customers.Controller;
using HotelManagePro.Utils.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagePro.Features.Customers.Menus
{
    public static class FindCustomerMenu
    {
        private static string[] menuOptions = new string[] {
        "Find by Customer ID",
        "Find by Email",
        "Find by Address",        
        "New Customer",
        "Back to Customer Menu"
    };

        private static CustomerController _customerController;

        public static void SetCustomerController(CustomerController customerController)
        {
            _customerController = customerController;
        }

        public static void ShowMenu()
        {
            MenuGenerator.ShowMenu("Find Customer", menuOptions, ExecuteSelectedOption);
        }

        private static void ExecuteSelectedOption(int selectedIndex)
        {
            switch (selectedIndex)
            {
                case 0:
                    _customerController.FindCustomerById();
                    break;
                case 1:
                    _customerController.FindCustomerByEmail();
                    break;
                case 2:
                    _customerController.FindCustomerByAddress();
                    break;
                case 3:
                    // New Customer                            
                    break;
                case 4:
                    // Back to Customer menu        
                    break;
                default:
                    break;
            }
        }
    }
}
