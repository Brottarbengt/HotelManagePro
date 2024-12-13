using HotelManagePro.Features.Customers.Models;
using HotelManagePro.Features.Invoices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagePro.Features.Bookings.Models
{
    internal class Booking
    {
        public int BookingId { get; set; }
        public Customer Customer { get; set; }
        public Invoice Invoice { get; set; }  // IsPaid can hämtas via Invoice
        public DateOnly ArrivalDate { get; set; }
        public DateOnly DepartureDate { get; set; }
    }
}
