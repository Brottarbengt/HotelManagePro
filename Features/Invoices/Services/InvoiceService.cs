using HotelManagePro.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManagePro.Features.Invoices.Models;
using HotelManagePro.Features.Rooms.Models;
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
        return _dbContext.Invoices.ToList();
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
        return _dbContext.Invoices
            .Include(i => i.Booking)
                .ThenInclude(b => b.Customer)
            .Where(i => i.Booking.Customer.Email == email)
            .OrderByDescending(i => i.Booking.ArrivalDate)
            .ToList();
    }

    public List<Invoice> GetUnpaidInvoices()
    {
        return _dbContext.Invoices
            .Include(i => i.Booking)
                .ThenInclude(b => b.Customer)
            .Where(i => !i.IsPaid)
            .OrderBy(i => i.Booking.ArrivalDate)
            .ToList();
    }
}
