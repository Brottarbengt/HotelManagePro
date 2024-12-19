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
    public class Rooms
    {
        public int RoomsId { get; set; }
        public TypeOfRoom RoomType { get; set; }
        public int RoomNumber { get; set; }
        public double Size { get; set; }
        public bool IsActive { get; set; }
        public Bookings Booking { get; set; }
        public int ExtraBeds { get; set; } // En beräknande prop, om type och size > smth = NrOfBeds. Vad händer med price

    }
}
