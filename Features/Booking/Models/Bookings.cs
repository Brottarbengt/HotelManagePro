using HotelManagePro.Features.Customer.Models;
using HotelManagePro.Features.Invoice.Models;
using HotelManagePro.Features.Room.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagePro.Features.Booking.Models
{
    internal class Bookings
    {
        public int BookingId { get; set; }
        public Customers Customer { get; set; }
        public Invoices Invoice { get; set; }  // IsPaid can hämtas via Invoice
        public DateOnly ArrivalDate { get; set; }
        public DateOnly DepartureDate { get; set; }
        public List<Rooms> Rooms { get; set; }
    }
}
