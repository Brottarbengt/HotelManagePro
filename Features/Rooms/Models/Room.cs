using HotelManagePro.Features.Bookings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagePro.Features.Rooms.Models
{
    public enum TypeOfRoom
    {
        Single,
        Double
    }
    internal class Room
    {
        public int RoomId { get; set; }
        public TypeOfRoom RoomType { get; set; }
        public int RoomNumber { get; set; }
        public double Size { get; set; }
        public bool IsActive { get; set; }
        public Booking Booking { get; set; }

    }
}
