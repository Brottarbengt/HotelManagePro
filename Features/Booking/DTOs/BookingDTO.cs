using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagePro.Features.Booking.DTOs
{
    public class BookingDTO
    {
        public int BookingId { get; set; }
        public string CustomerName { get; set; }
        public DateOnly ArrivalDate { get; set; }
        public DateOnly DepartureDate { get; set; }
        public List<string> RoomDetails { get; set; }
        public bool IsPaid { get; set; }
        public decimal TotalCost { get; set; }
    }
}
