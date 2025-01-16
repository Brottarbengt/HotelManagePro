using HotelManagePro.Features.Bookings.Models;
using HotelManagePro.Features.Bookings.Services;
using HotelManagePro.Features.Customers.Models;
using HotelManagePro.Features.Rooms.Models;
using HotelManagePro.Features.Rooms.Services;
using HotelManagePro.Utils;
using Spectre.Console;
using HotelManagePro.Features.Invoices.Services;
using HotelManagePro.Features.Customers.Controller;
using HotelManagePro.Features.Customers.Services;


namespace HotelManagePro.Features.Bookings.Controller;

public class BookingController
{
    private readonly BookingService _bookingService;
    private readonly RoomService _roomService;
    private readonly InvoiceService _invoiceService;
    private readonly CustomerController _customerController;
    private readonly CustomerService _customerService;
    private readonly FindCustomerForBooking _findCustomerForBooking;
    private readonly BookingValidator _bookingValidator;
    private readonly string[] choices =
                            [
                                "Find by ID",
                                "Find by Email",
                                "Find by Phone",
                                "Back"
                            ];

    public BookingController(
        BookingService bookingService, 
        RoomService roomService,
        InvoiceService invoiceService,
        CustomerController customerController,
        CustomerService customerService,
        FindCustomerForBooking findCustomerForBooking,
        BookingValidator bookingValidator)
    {
        _bookingService = bookingService;
        _roomService = roomService;
        _invoiceService = invoiceService;
        _customerController = customerController;
        _customerService = customerService;
        _findCustomerForBooking = findCustomerForBooking;
        _bookingValidator = bookingValidator;
    }

    

    public void CreateNewBooking()
    {
        Customer? customer = GetBookingCustomer();
        if (customer == null)
        {
            return;
        }

        var (arrivalDate, departureDate) = GetDates();
        var numberOfGuests = GetNumberOfGuests();

        List<Room> availableRooms = _roomService.GetAvailableRooms(arrivalDate, departureDate);
        var selectedRooms = RoomPicker.PickRooms(availableRooms);
        
        if (selectedRooms.Count == 0)
        {
            Console.WriteLine("No rooms selected. Booking cancelled.");
            return;
        }

        var extraBeds = GetNumberOfExtraBeds(numberOfGuests, selectedRooms);
        
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
    private Customer? GetBookingCustomer()
    {
        while (true)
        {
            Console.Clear();
            AnsiConsole.MarkupLine("\n[blue]Existing Customer or New Customer?[/]");
            AnsiConsole.MarkupLine("Press [green]Y[/] to find existing customer or [green]N[/] to create new customer");
            
            var key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Escape)
            {
                Console.WriteLine("\nBooking cancelled.");
                return null;
            }

            if (key.KeyChar.ToString().Equals("n", StringComparison.CurrentCultureIgnoreCase))
            {
                var customer = _customerController.CreateNewCustomer();
                if (customer == null)
                {
                    Console.WriteLine("Customer creation cancelled. Booking cancelled.");
                    return null;
                }
                return customer;
            }
            else if (key.KeyChar.ToString().Equals("y", StringComparison.CurrentCultureIgnoreCase))
            {
                while (true)
                {
                    Console.Clear();
                    AnsiConsole.MarkupLine("\n[blue]Find Customer[/]");
                    var choice = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("How would you like to find the customer?")
                            .AddChoices(choices));

                    int? customerId = null;
                    switch (choice)
                    {
                        case "Find by ID":
                            customerId = _findCustomerForBooking.FindById();
                            break;
                        case "Find by Email":
                            customerId = _findCustomerForBooking.FindByEmail();
                            break;
                        case "Find by Phone":
                            customerId = _findCustomerForBooking.FindByPhone();
                            break;
                        case "Back":
                            break;
                    }

                    if (customerId.HasValue)
                    {
                        var customer = _customerService.GetCustomerById(customerId.Value);
                        if (customer != null)
                        {
                            return customer;
                        }
                    }
                    else if (choice == "Back")
                    {
                        break;
                    }
                }
            }
        }
    }

    public void UpdateBooking()
    {
        try
        {
            ShowBookingsByDateRange();
            Console.WriteLine("\nSelect date range for the booking you want to update:");
            var (startDate, endDate) = GetDates();
            
            var bookings = _bookingService.GetBookingsByDateRange(startDate, endDate);
            if (bookings.Count == 0) return;

            int bookingId;
            while (true)
            {
                Console.WriteLine("\nEnter the ID of the booking to update (or press ESC to cancel):");
                
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Escape)
                    return;

                Console.Write("Booking ID: ");
                var input = Console.ReadLine();
                try
                {
                    bookingId = _bookingValidator.GetValidBookingId(input, bookings);
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Invalid input: {ex.Message}. Please try again.");
                }
            }

            var booking = bookings.First(b => b.BookingId == bookingId);
            
            while (true)
            {
                Console.Clear();
                DisplayBooking(booking);
                
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("What would you like to update?")
                        .AddChoices([
                        
                            "Update Dates",
                            "Update Number of Guests",
                            "Update Rooms",
                            "Save and Exit",
                            "Cancel Changes"
                        ]));

                switch (choice)
                {
                    case "Update Dates":
                        var (newArrival, newDeparture) = GetDates();
                        booking.ArrivalDate = newArrival;
                        booking.DepartureDate = newDeparture;
                        break;

                    case "Update Number of Guests":
                        booking.NumberOfGuests = GetNumberOfGuests();
                        break;

                    case "Update Rooms":
                        var availableRooms = _roomService.GetAvailableRooms(booking.ArrivalDate, booking.DepartureDate);
                        availableRooms.AddRange(booking.Rooms); // Include currently booked rooms
                        var selectedRooms = RoomPicker.PickRooms(availableRooms.Distinct().ToList());
                        
                        if (selectedRooms.Count == 0)
                        {
                            Console.WriteLine("No rooms selected. Keeping current rooms.");
                            Console.WriteLine("\nPress any key to continue...");
                            Console.ReadKey(true);
                            continue;
                        }
                        
                        booking.Rooms = selectedRooms;
                        break;

                    case "Save and Exit":
                        var extraBeds = GetNumberOfExtraBeds(booking.NumberOfGuests, booking.Rooms);
                        var basePrice = booking.Rooms.Sum(r => r.Price);
                        var guestPrice = booking.NumberOfGuests * 100;
                        var extraBedPrice = extraBeds * 150;
                        var totalPrice = basePrice + guestPrice + extraBedPrice;

                        booking.Invoice.TotalSum = totalPrice;
                        booking.Invoice.ExtraBeds = extraBeds;
                        
                        Console.Clear();
                        Console.WriteLine("Review updated booking details:");
                        Console.WriteLine(new string('-', 40));
                        DisplayBooking(booking);
                        Console.WriteLine(new string('-', 40));
                        
                        Console.Write("\nIs this correct? Y to confirm, any other to go back to update menu (bajskorv for example).): ");
                        var confirm = Console.ReadLine()?.Trim().ToUpper();
                        
                        if (confirm == "Y")
                        {
                            _bookingService.UpdateBooking(booking);
                            Console.WriteLine("Booking updated successfully!");
                            Console.WriteLine("\nPress any key to continue...");
                            Console.ReadKey(true);
                            return;
                        }
                        else
                        {
                            Console.WriteLine("Returning to update menu...");
                            Console.WriteLine("\nPress any key to continue...");
                            Console.ReadKey(true);
                            continue;
                        }

                    case "Cancel Changes":
                        return;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(true);
        }
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
            ShowBookingsByDateRange();
            Console.WriteLine("\nSelect date range for the booking you want to remove:");
            var (startDate, endDate) = GetDates();
            
            var bookings = _bookingService.GetBookingsByDateRange(startDate, endDate);
            if (bookings.Count == 0) return;

            int bookingId;
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter the ID of the booking to remove:");
                    string? input = Console.ReadLine();
                    bookingId = _bookingValidator.GetValidBookingId(input, bookings);
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

                if (bookings.Count == 0)
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

    public  void DisplayBooking(Booking booking)
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

    public  int GetNumberOfGuests()
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

    public  int GetNumberOfExtraBeds(int numberOfGuests, List<Room> selectedRooms)
    {
        var standardBeds = selectedRooms.Sum(r => r.RoomType == TypeOfRoom.Single ? 1 : 2);
        var maxExtraBeds = selectedRooms.Count * 1; // 1 extra bed per room maximum
        var neededExtraBeds = Math.Max(0, numberOfGuests - standardBeds);

        if (neededExtraBeds == 0)
            return 0;

        while (true)
        {
            Console.WriteLine($"\nStandard beds in selected rooms: {standardBeds}");
            Console.WriteLine($"Extra beds possible (0 - {maxExtraBeds})");
            Console.Write("How many extra beds would you like to add? ");
            
            if (int.TryParse(Console.ReadLine(), out int requestedBeds))
            {
                if (requestedBeds < 0 || requestedBeds > maxExtraBeds)
                {
                    Console.WriteLine($"Please enter a number between 0 and {maxExtraBeds}");
                    continue;
                }

                if (standardBeds + requestedBeds < numberOfGuests)
                {
                    Console.WriteLine($"\nWarning: Not enough beds for {numberOfGuests} guests.");
                    Console.WriteLine($"Total beds would be: {standardBeds + requestedBeds}");
                    Console.Write("Would you like to try again? (y/n): ");
                    
                    if (Console.ReadLine()?.Trim().ToLower() == "y")
                        continue;
                }

                return requestedBeds;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }
        }
    }

    private  (DateOnly arrivalDate, DateOnly departureDate) GetDates()
    {
        Console.WriteLine("\nBooking Dates Selection");
        var today = DateOnly.FromDateTime(DateTime.Now);
        DateOnly arrivalDate;
        
        while (true)
        {
            arrivalDate = DatePicker.PickDate("Select Arrival Date");
            if (arrivalDate >= today)
                break;
            
            Console.WriteLine("Arrival date cannot be in the past. Please select a future date.");
        }

        DateOnly departureDate;
        while (true)
        {
            departureDate = DatePicker.PickDate("Select Departure Date");
            if (departureDate > arrivalDate)
                break;
            
            Console.WriteLine("Departure date must be after arrival date. Please select a later date.");
        }

        return (arrivalDate, departureDate);
    }

    public void ShowBookingsByDateRange()
    {
        Console.WriteLine("\nSelect date range to show bookings:");
        var (startDate, endDate) = GetDates();
        
        var bookings = _bookingService.GetBookingsByDateRange(startDate, endDate);
        if (bookings.Count == 0)
        {
            Console.WriteLine($"\nNo bookings found between {startDate:d} and {endDate:d}");
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(true);
            return;
        }

        Console.WriteLine($"\nBookings between {startDate:d} and {endDate:d}:");
        foreach (var booking in bookings)
        {
            DisplayBooking(booking);
        }
        
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey(true);
    }
}
