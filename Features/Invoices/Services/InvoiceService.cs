using HotelManagePro.Database;
using HotelManagePro.Features.Invoices.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagePro.Features.Invoices.Services;

public class InvoiceService
{
    private readonly ApplicationDbContext _dbContext;

    public InvoiceService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Invoice CreateInvoice(double totalSum, int extraBeds)
    {
        var invoice = new Invoice
        {
            TotalSum = totalSum,
            IsPaid = false,
            ExtraBeds = extraBeds
        };
        return invoice;
    }

    public List<Invoice> GetAllInvoices()
    {
        return _dbContext.Invoices
            .Include(i => i.BookingId)
            .ToList();
    }

    public Invoice? GetInvoiceById(int id)
    {
        return _dbContext.Invoices
            .FirstOrDefault(i => i.InvoiceId == id);
    }

    public bool MarkAsPaid(int invoiceId)
    {
        var invoice = _dbContext.Invoices.Find(invoiceId);
        if (invoice == null) return false;

        invoice.IsPaid = true;
        _dbContext.SaveChanges();
        return true;
    }


    public void UpdateInvoice(Invoice invoice)
    {
        _dbContext.Update(invoice);
        _dbContext.SaveChanges();
    }

    public List<Invoice> GetInvoicesByCustomerEmail(string? email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return [];
        }

        var bookingsWithCustomerEmail = _dbContext.Bookings
            .Where(b => b.Customer.Email.ToLower() == email.ToLower())
            .Select(b => b.BookingId);

        return _dbContext.Invoices
            .Where(i => bookingsWithCustomerEmail.Contains(i.BookingId))
            .ToList();
    }

    public List<Invoice> GetUnpaidInvoices()
    {
        return _dbContext.Invoices
            .Where(i => i.IsPaid == false)
            .ToList();
    }
}
