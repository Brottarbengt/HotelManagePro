using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagePro.Features.Invoice
{
    public class InvoiceMenu
    {
        private static string[] menuOptions = new string[] {
            "Edit Invoice", // Shows all invoices first
            "Show All Invoices", // Showing all bookings with invoice or just invoices?
            "Edit Invoice", // Ska ShowAll() (en månad?) framöver ovan prompt
            "Back to Top Menu"
        };
    }
}
