using HotelManagePro.Features.Customers.Services;
using HotelManagePro.Features.Invoices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManagePro.Features.Invoices.Models;

namespace HotelManagePro.Features.Invoices.Controller;

public class InvoiceController
{
    private readonly InvoiceService _invoiceService;

    public InvoiceController(InvoiceService invoiceService)
    {
        _invoiceService = invoiceService;
    }

    public void ShowAllInvoices()
    {
        var invoices = _invoiceService.GetAllInvoices();
        if (!invoices.Any())
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
        
        if (invoice.Booking != null)
        {
            Console.WriteLine($"Customer ID: {invoice.Booking.Customer.CustomersId}");
            Console.WriteLine($"Customer Email: {invoice.Booking.Customer.Email}");
            Console.WriteLine($"Arrival Date: {invoice.Booking.ArrivalDate:d}");
            Console.WriteLine($"Departure Date: {invoice.Booking.DepartureDate:d}");
        }
        
        Console.WriteLine(new string('-', 40));
    }
}
