using HotelManagePro.Features.Bookings.Models;
using HotelManagePro.Features.Bookings.Services;
using HotelManagePro.Features.Customers.Models;
using HotelManagePro.Features.Rooms.Models;
using HotelManagePro.Features.Rooms.Services;
using HotelManagePro.Utils;
using Spectre.Console;
using HotelManagePro.Features.Invoices.Services;
using HotelManagePro.Features.Customers.Controller;

namespace HotelManagePro.Features.Bookings.Controller;

public class BookingController
{
    private readonly BookingService _bookingService;
    private readonly RoomService _roomService;
    private readonly InvoiceService _invoiceService;
    private readonly CustomerController _customerController;

    public BookingController(
        BookingService bookingService, 
        RoomService roomService,
        InvoiceService invoiceService,
        CustomerController customerController)
    {
        _bookingService = bookingService;
        _roomService = roomService;
        _invoiceService = invoiceService;
        _customerController = customerController;
    }

    
    public void CreateNewBooking()
    {

        //  New Booking -> Väljer Datum -> Visar lediga rum, väljer rum -> Ta Personuppgifter -> Bekräftar booking
        
        var arrivalDate = DatePicker.PickDate();
        var departureDate = DatePicker.PickDate();

        // Fråga om extra sängar

        List<Room> availableRooms = _roomService.GetAvailableRooms(arrivalDate, departureDate);
        var rooms = RoomPicker.PickRooms(availableRooms);        
        
        var newInvoice = _invoiceService.CreateInvoice();

        //Is info correct? yes/No -> EditCustomer() or
        //ConfirmBooking() else: InputNewCustomer()

        //Var newInvoice = CreateInvoice(Rooms, arrivalDate, departureDate)

        var customer = _customerController.CreateNewCustomer();

        var newBooking = new Booking
        {
            ArrivalDate = arrivalDate,
            DepartureDate = departureDate,
            Invoice = newInvoice,
            Rooms = rooms,
            Customer = customer
        };

        // save to database

    }
    
    
    public void EditBooking()
    {

    }
    public void ShowAllBookings()
    {
        try
        {
            var bookings = _bookingService.GetAllBookings();
            if (bookings.Count == 0)
            {
                Console.WriteLine("No bookings found.");
                return;
            }

            Console.WriteLine("All bookings:");
            foreach (var booking in bookings)
            {
                DisplayBooking(booking);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    public void RemoveBooking()
    {
        try
        {
            ShowAllBookings();
            var bookings = _bookingService.GetAllBookings();

            int bookingId;
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter the ID of the booking to remove:");
                    string? input = Console.ReadLine();
                    bookingId = BookingValidator.GetValidBookingId(input, bookings);
                    break; 
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Invalid input: {ex.Message}. Please try again.");
                }
            }

            _bookingService.RemoveBooking(bookingId);
            Console.WriteLine("Booking removed successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }
    public void SearchActiveBookingByEmail()
    {
        try
        {
            var email = GetValidatedEmailInput();
            var bookings = _bookingService.FindActiveBookingByEmail(email);

            if (!bookings.Any())
            {
                Console.WriteLine("No active bookings found for the provided email.");
                return;
            }

            Console.WriteLine("Active bookings:");
            foreach (var booking in bookings)
            {
                DisplayBooking(booking);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
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


    public void DisplayBooking(Booking booking)
    {
        Console.WriteLine($"Booking ID: {booking.BookingId}");
        Console.WriteLine($"Customer: {booking.Customer.FirstName} {booking.Customer.LastName}");
        Console.WriteLine($"Arrival Date: {booking.ArrivalDate}");
        Console.WriteLine($"Departure Date: {booking.DepartureDate}");
        Console.WriteLine($"Rooms: {string.Join(", ", booking.Rooms.Select(r => $"Room {r.RoomNumber} ({r.RoomType})"))}");
        Console.WriteLine($"Is Paid: {booking.Invoice?.IsPaid ?? false}");
        Console.WriteLine($"Total Cost: {booking.Invoice?.TotalSum ?? 0}");
        Console.WriteLine(new string('-', 40));
    }
}
