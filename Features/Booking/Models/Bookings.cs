using HotelManagePro.Features.Customer.Models;
using HotelManagePro.Features.Invoice.Models;
using HotelManagePro.Features.Room.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagePro.Features.Booking.Models
{
    public class Bookings
    {
      
        public int BookingsId { get; set; }
        public Invoices Invoice { get; set; }  // IsPaid can hämtas via Invoice
        public DateOnly ArrivalDate { get; set; }
        public DateOnly DepartureDate { get; set; }
        public List<Rooms> Rooms { get; set; }
    }
}
