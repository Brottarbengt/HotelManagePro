using HotelManagePro.Features.Bookings.Models;
using HotelManagePro.Features.Rooms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagePro.Features.Invoices.Models
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public required double TotalSum { get; set; }
        public required bool IsPaid { get; set; } = false;
        public int ExtraBeds { get; set; }
        public Booking? Booking { get; set; }
    }
}
