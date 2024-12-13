using HotelManagePro.Features.Rooms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagePro.Features.Invoices.Models
{
    internal class Invoice
    {
        public int InvoiceId { get; set; }
        public int TotalSum { get; set; }
        public bool IsPaid { get; set; }
        public Room Room { get; set; }
    }
}
