using HotelManagePro.Features.Room.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagePro.Features.Invoice.Models
{
    public class Invoices
    {
        public int InvoicesId { get; set; }
        public required int TotalSum { get; set; }
        public required bool IsPaid { get; set; }
       
    }
}
