using HotelManagePro.Features.Invoices.Services;
using HotelManagePro.Features.Invoices.Models;
using HotelManagePro.Features.Bookings.Services;
using Spectre.Console;

namespace HotelManagePro.Features.Invoices.Controller;

public class InvoiceController
{
    private readonly InvoiceService _invoiceService;
    private readonly BookingService _bookingService;

    public InvoiceController(
        InvoiceService invoiceService,
        BookingService bookingService)
    {
        _invoiceService = invoiceService;
        _bookingService = bookingService;
    }

    public void ShowAllInvoices()
    {
        var invoices = _invoiceService.GetAllInvoices();
        if (invoices.Count == 0)
        {
            Console.WriteLine("No invoices found.");
            return;
        }

        foreach (var invoice in invoices)
        {
            DisplayInvoice(invoice);
        }
    }

    public void SearchInvoice()
    {
        Console.Write("Enter invoice ID: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            var invoice = _invoiceService.GetInvoiceById(id);
            if (invoice != null)
            {
                DisplayInvoice(invoice);
            }
            else
            {
                Console.WriteLine("Invoice not found.");
            }
        }
    }

    public void MarkInvoiceAsPaid()
    {
        Console.Write("Enter invoice ID: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            var success = _invoiceService.MarkAsPaid(id);
            Console.WriteLine(success ? "Invoice marked as paid." : "Failed to update invoice.");
        }
    }

    public void UpdateInvoice()
    {
        try
        {
            Console.Write("\nEnter Invoice ID: ");
            if (!int.TryParse(Console.ReadLine(), out int invoiceId))
            {
                Console.WriteLine("Invalid invoice ID.");
                return;
            }

            var invoice = _invoiceService.GetInvoiceById(invoiceId);
            if (invoice == null)
            {
                Console.WriteLine("Invoice not found.");
                return;
            }

            Console.Clear();
            Console.WriteLine("Current invoice details:");
            Console.WriteLine(new string('-', 40));
            DisplayInvoice(invoice);
            Console.WriteLine(new string('-', 40));

            Console.Write($"\nUpdate price? Current price is {invoice.TotalSum:C} (y/n): ");
            if (Console.ReadLine()?.Trim().ToUpper() == "Y")
            {
                Console.Write($"Enter new price ({invoice.TotalSum}): ");
                var input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    input = invoice.TotalSum.ToString();
                }
                
                if (double.TryParse(input, out double newPrice))
                {
                    invoice.TotalSum = newPrice;
                }
                else
                {
                    Console.WriteLine("Invalid price format. Price not updated.");
                }
            }

            if (!invoice.IsPaid)
            {
                Console.Write("\nMark invoice as paid? (y/n): ");
                if (Console.ReadLine()?.Trim().ToUpper() == "Y")
                {
                    invoice.IsPaid = true;
                }
            }

            Console.Clear();
            Console.WriteLine("Review updated invoice details:");
            Console.WriteLine(new string('-', 40));
            DisplayInvoice(invoice);
            Console.WriteLine(new string('-', 40));

            Console.Write("\nSave these changes? Y to confirm, any other key to cancel: ");
            if (Console.ReadLine()?.Trim().ToUpper() == "Y")
            {
                _invoiceService.UpdateInvoice(invoice);
                Console.WriteLine("Invoice updated successfully!");
            }
            else
            {
                Console.WriteLine("Update cancelled.");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(true);
        }
    }
    
    public void FindInvoice()
    {
        while (true)
        {
            Console.Clear();
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("How would you like to find the invoice?")
                    .AddChoices([
                        "Find by Customer Email",
                        "Show All Unpaid Invoices",
                        "Back to Menu"
                    ]));

            switch (choice)
            {
                case "Find by Customer Email":
                    Console.Write("\nEnter customer email: ");
                    var email = Console.ReadLine();
                    var invoicesByEmail = _invoiceService.GetInvoicesByCustomerEmail(email);
                    
                    if (invoicesByEmail.Count == 0)
                    {
                        Console.WriteLine("No invoices found for this email.");
                    }
                    else
                    {
                        foreach (var invoice in invoicesByEmail)
                        {
                            DisplayInvoice(invoice);
                        }
                    }
                    break;

                case "Show All Unpaid Invoices":
                    var unpaidInvoices = _invoiceService.GetUnpaidInvoices();
                    if (unpaidInvoices.Count == 0)
                    {
                        Console.WriteLine("No unpaid invoices found.");
                    }
                    else
                    {
                        foreach (var invoice in unpaidInvoices)
                        {
                            DisplayInvoice(invoice);
                        }
                    }
                    break;

                case "Back to Menu":
                    return;
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(true);
        }
    }

    private void DisplayInvoice(Invoice invoice)
    {
        Console.WriteLine($"Invoice ID: {invoice.InvoiceId}");
        Console.WriteLine($"Total Sum: {invoice.TotalSum:C}");
        Console.WriteLine($"Status: {(invoice.IsPaid ? "Paid" : "Unpaid")}");
        Console.WriteLine($"Extra Beds: {invoice.ExtraBeds}");
        
        var booking = _bookingService.GetBookingById(invoice.BookingId);
        if (booking != null)
        {
            Console.WriteLine($"\nBooking Details:");
            Console.WriteLine($"Customer: {booking.Customer.FirstName} {booking.Customer.LastName}");
            Console.WriteLine($"Customer Email: {booking.Customer.Email}");
            Console.WriteLine($"Arrival Date: {booking.ArrivalDate:d}");
            Console.WriteLine($"Departure Date: {booking.DepartureDate:d}");
            Console.WriteLine($"Number of Guests: {booking.NumberOfGuests}");
            Console.WriteLine($"Rooms: {string.Join(", ", booking.Rooms.Select(r => r.RoomNumber))}");
        }
        
        Console.WriteLine(new string('-', 40));
    }
}
