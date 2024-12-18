using HotelManagePro.Features.Booking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagePro.Features.Room.Models
{
    public enum TypeOfRoom
    {
        Single,
        Double
    }
    internal class Rooms
    {
        public int RoomId { get; set; }
        public TypeOfRoom RoomType { get; set; }
        public int RoomNumber { get; set; }
        public double Size { get; set; }
        public bool IsActive { get; set; }
        public Bookings Booking { get; set; }

    }
}
