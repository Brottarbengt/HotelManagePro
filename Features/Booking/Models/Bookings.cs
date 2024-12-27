using HotelManagePro.Features.Customer.Models;
using HotelManagePro.Features.Invoice.Models;
using HotelManagePro.Features.Room.Models;


namespace HotelManagePro.Features.Booking.Models
{
    public class Bookings
    {
      
        public int BookingsId { get; set; }
        public Invoices Invoice { get; set; }  // IsPaid can hämtas via Invoice
        public DateOnly ArrivalDate { get; set; }
        public DateOnly DepartureDate { get; set; }
        public List<Rooms> Rooms { get; set; }
        public Customers Customers { get; set; }
       
    }
}
