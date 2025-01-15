

namespace HotelManagePro.Features.Invoices.Models
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public required double TotalSum { get; set; }
        public required bool IsPaid { get; set; } = false;
        public int ExtraBeds { get; set; }
        public int BookingId { get; set; }
    }
}
