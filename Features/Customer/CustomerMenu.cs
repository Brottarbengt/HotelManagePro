using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagePro.Features.Customer
{
    public class CustomerMenu
    {
        private static string[] menuOptions = new string[] {
            "New Customer",
            "Search for Customer", // Undermeny med vad man vill söka på för Attrib? alt. söker igenom alla attribut på något som stämmer?
            "Show All Customers",
            "Remove Customers (soft Delete)"
            "DELETE Customer, (WARNING! Hard Delete, only if asked to.)", // Ska ShowAllCustomers
            "Edit Customer", // Ska ShowAllCustomers, Ska man kunna välja på samma sätt som menyn, dvs använda MenuGenerator?
            "Back to Top Menu"
        };
    }
}
