using HotelManagePro.Features.Customers.Models;
using HotelManagePro.Features.Invoices.Models;
using HotelManagePro.Features.Rooms.Models;


namespace HotelManagePro.Features.Bookings.Models;

public class Booking
{
  
    public int BookingsId { get; set; }
    public required Invoice Invoice { get; set; }  // IsPaid can hämtas via Invoice
    public required DateOnly ArrivalDate { get; set; }
    public required DateOnly DepartureDate { get; set; }
    public required List<Room> Rooms { get; set; }
    public Customer Customer { get; set; }
   
}
