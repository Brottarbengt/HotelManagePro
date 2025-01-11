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
        var arrivalDate = DatePicker.PickDate();
        var departureDate = DatePicker.PickDate();
        var numberOfGuests = GetNumberOfGuests();

        List<Room> availableRooms = _roomService.GetAvailableRooms(arrivalDate, departureDate);
        var selectedRooms = RoomPicker.PickRooms(availableRooms);
        
        if (!selectedRooms.Any())
        {
            Console.WriteLine("No rooms selected. Booking cancelled.");
            return;
        }

        var extraBeds = GetNumberOfExtraBeds(numberOfGuests, selectedRooms);
        
        var customer = _customerController.CreateNewCustomer();
        if (customer == null)
        {
            Console.WriteLine("Customer creation cancelled. Booking cancelled.");
            return;
        }

        var basePrice = selectedRooms.Sum(r => r.Price);
        var guestPrice = numberOfGuests * 100;
        var extraBedPrice = extraBeds * 150;
        var totalPrice = basePrice + guestPrice + extraBedPrice;

        var newInvoice = _invoiceService.CreateInvoice(totalPrice, extraBeds);

        var newBooking = new Booking
        {
            ArrivalDate = arrivalDate,
            DepartureDate = departureDate,
            NumberOfGuests = numberOfGuests,
            Invoice = newInvoice,
            Rooms = selectedRooms,
            Customer = customer
        };

        _bookingService.CreateNewBooking(newBooking);
        Console.WriteLine($"\nBooking created successfully!");
        DisplayBooking(newBooking);
    }
    
    
    public void UpdateBooking()
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
        while (true)
        {
            Console.WriteLine("\nPress ESC to return to menu or search for bookings:");
            
            while (Console.KeyAvailable) Console.ReadKey(true);
            
            var key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Escape)
                return;

            try
            {
                var email = CustomerValidator.GetValidatedEmail();
                if (email == null) continue;

                var bookings = _bookingService.FindActiveBookingByEmail(email);

                if (!bookings.Any())
                {
                    Console.WriteLine("No active bookings found for the provided email.");
                    continue;
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

    private int GetNumberOfGuests()
    {
        while (true)
        {
            Console.Write("Enter number of guests (maximum 6): ");
            if (int.TryParse(Console.ReadLine(), out int numberOfGuests))
            {
                if (numberOfGuests <= 0)
                {
                    Console.WriteLine("Number of guests must be at least 1.");
                    continue;
                }
                
                if (numberOfGuests > 6)
                {
                    Console.WriteLine("Maximum 6 guests per booking. Please make separate bookings for larger groups.");
                    continue;
                }
                
                return numberOfGuests;
            }
            Console.WriteLine("Invalid input. Please enter a number.");
        }
    }

    private int GetNumberOfExtraBeds(int numberOfGuests, List<Room> selectedRooms)
    {
        var standardBeds = selectedRooms.Sum(r => r.RoomType == TypeOfRoom.Single ? 1 : 2);
        var maxExtraBeds = selectedRooms.Count * 1; // 1 extra bed per room maximum
        var neededExtraBeds = Math.Max(0, numberOfGuests - standardBeds);

        if (neededExtraBeds == 0)
            return 0;

        if (neededExtraBeds > maxExtraBeds)
        {
            Console.WriteLine($"Warning: Not enough beds for {numberOfGuests} guests.");
            Console.WriteLine($"Standard beds: {standardBeds}, Maximum extra beds possible: {maxExtraBeds}");
            Console.WriteLine("Please select more rooms or reduce number of guests.");
            return 0;
        }

        Console.WriteLine($"\nYou need {neededExtraBeds} extra bed(s) for {numberOfGuests} guests.");
        Console.Write("Would you like to add extra beds? (y/n): ");
        
        return Console.ReadLine()?.Trim().ToLower() == "y" ? neededExtraBeds : 0;
    }
}
