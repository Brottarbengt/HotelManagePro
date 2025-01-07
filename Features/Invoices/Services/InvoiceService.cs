using HotelManagePro.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManagePro.Features.Invoices.Models;
using HotelManagePro.Features.Rooms.Models;

namespace HotelManagePro.Features.Invoices.Services;

public class InvoiceService
{
    private readonly ApplicationDbContext _dbContext;

    public InvoiceService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Invoice CreateInvoice()
    {
        var invoice = new Invoice
        {
            TotalSum = 0,
            IsPaid = false
        };
        return invoice;
    }
    public double CalculateTotalSum(double price, int extraBeds)
    {
        return rooms.Sum(room => room.Price);
    }
}
