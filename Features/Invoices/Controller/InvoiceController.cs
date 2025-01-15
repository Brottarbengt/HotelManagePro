using HotelManagePro.Features.Invoices.Services;
using HotelManagePro.Features.Invoices.Models;
using HotelManagePro.Features.Bookings.Services;

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
