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
        public int InvoicesId { get; set; }
        public required int TotalSum { get; set; }
        public required bool IsPaid { get; set; }
        public int ExtraBeds { get; set; }
        public Booking Booking { get; set; }

    }
}
