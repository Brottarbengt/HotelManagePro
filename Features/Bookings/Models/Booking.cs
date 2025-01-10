using HotelManagePro.Features.Customers.Models;
using HotelManagePro.Features.Invoices.Models;
using HotelManagePro.Features.Rooms.Models;


namespace HotelManagePro.Features.Bookings.Models;

public class Booking
{
  
    public int BookingId { get; set; }
    public required Invoice Invoice { get; set; }  
    public required DateOnly ArrivalDate { get; set; }
    public required DateOnly DepartureDate { get; set; }
    public required List<Room> Rooms { get; set; }
    public required Customer Customer { get; set; }
    public int NumberOfGuests { get; set; }
   
}
