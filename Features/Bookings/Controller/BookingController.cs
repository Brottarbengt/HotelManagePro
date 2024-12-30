using HotelManagePro.Database;
using HotelManagePro.Features.Bookings.Models;
using HotelManagePro.Features.Bookings.Services;
using HotelManagePro.Features.Invoices.Models;
using HotelManagePro.Features.Rooms.Models;
using HotelManagePro.Utils;
using HotelManagePro.Features.Customers.Models;
using Spectre.Console;
using HotelManagePro.Features.Rooms.Services;

namespace HotelManagePro.Features.Bookings.Controller;

public class BookingController
{
    private readonly BookingService _bookingService;

    public BookingController(BookingService bookingService)
    {
        _bookingService = bookingService;
    }

    private readonly RoomService _roomService;
    public BookingController(RoomService roomService)
    {
        _roomService = roomService;
    }

    public void CreateNewBooking()
    {
        
        //  New Booking -> Väljer Datum -> Visar lediga rum, väljer rum -> Ta Personuppgifter -> Bekräftar booking
        var arrivalDate = DatePicker.PickDate();
        var departureDate = DatePicker.PickDate();

        List<Room> availableRooms = _roomService.GetAvailableRooms(arrivalDate, departureDate);

        var rooms = RoomPicker.PickRoom(availableRooms);
                
        //Is info correct? yes/No -> EditCustomer() or
        //ConfirmBooking() else: InputNewCustomer()

        //Var newInvoice = CreateInvoice(Rooms, arrivalDate, departureDate)

        var newBooking = new Booking
        {
            ArrivalDate = arrivalDate,
            DepartureDate = departureDate,
            Invoice = newInvoice,
            Rooms = rooms,
            Customer = cutomer
        };
        
        // save to database

    }
    private string GetValidatedEmailInput()
        {
            while (true)
            {
                Console.Write("Ange e-postadress: ");
                var email = Console.ReadLine()?.Trim();

                if (CustomerValidator.IsValidEmail(email))
                {
                    return email!;
                }

                Console.WriteLine("Ogiltig e-postadress. Försök igen.");
            }
        }
    public void SearchBookingByEmail()
    {
        Console.WriteLine("::: SEARCH BY EMAIL :::");
        Console.WriteLine("Enter customer Email: ");
        // Call get email input method()
        // call bookingService.FindActiveBookingByEmail(string customerEmail)

    }

    public void ShowAllBookings()
    {

    }

    public void RemoveBooking()
    {
        // Needs to take input and find Booking, ?SearchByEmail()? then remove.
        // Next TO DO ?
    }

    public void EditBooking()
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
