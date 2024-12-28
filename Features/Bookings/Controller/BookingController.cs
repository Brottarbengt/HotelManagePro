using HotelManagePro.Database;
using HotelManagePro.Features.Bookings.Models;
using HotelManagePro.Features.Bookings.Services;
using HotelManagePro.Features.Invoices.Models;
using HotelManagePro.Features.Rooms.Models;
using HotelManagePro.Utils;

namespace HotelManagePro.Features.Bookings.Controller;

public class BookingController
{
    private readonly BookingService _bookingService;

    public BookingController(BookingService bookingService)
    {
        _bookingService = bookingService;
    }
    public void CreateNewBooking()
    {

        //  New Booking -> Väljer Datum -> Visar lediga rum, väljer rum -> Ta Personuppgifter -> Bekräftar booking
        var arrivalDate = DatePicker.PickDate();
        var departureDate = DatePicker.PickDate();

        // var rooms = RoomPicker(Dates) -> ShowAvailableRooms(Dates) -> Return rooms


        //var customer = CustomerInput() -> SearchOnEmail() -> if true -> ShowCustomerInfo() for validation,
        //Is info correct? yes/No -> EditCustomer() or
        //ConfirmBooking() else: InputNewCustomer()

        //Var Invoice = CreateInvoice(Rooms, arrivalDate, departureDate)

        var newBooking = new Booking
        {
            ArrivalDate = arrivalDate,
            DepartureDate = departureDate,

        };
        
        // save to database

    }

    public void SearchBookingByEmail()
    {

    }

    public void RemoveBooking(int bookingId)
    {

    }

    public void EditBooking(int bookingId)
    {
        
    }

    

    public void DisplayBooking(Booking booking)
    {
        Console.WriteLine($"Booking ID: {booking.BookingsId}");
        Console.WriteLine($"Customer: {booking.Customer.FirstName} {booking.Customer.LastName}");
        Console.WriteLine($"Arrival Date: {booking.ArrivalDate}");
        Console.WriteLine($"Departure Date: {booking.DepartureDate}");
        Console.WriteLine($"Rooms: {string.Join(", ", booking.Rooms.Select(r => $"Room {r.RoomNumber} ({r.RoomType})"))}");
        Console.WriteLine($"Is Paid: {booking.Invoice?.IsPaid ?? false}");
        Console.WriteLine($"Total Cost: {booking.Invoice?.TotalSum ?? 0}");
        Console.WriteLine(new string('-', 40));
    }
}
