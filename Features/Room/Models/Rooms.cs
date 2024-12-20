using HotelManagePro.Features.Booking.Models;


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
        public required int RoomNumber { get; set; }
        public required TypeOfRoom RoomType { get; set; }
        public required double Size { get; set; }
        public required bool IsActive { get; set; }
        public Bookings Booking { get; set; }
        public int ExtraBeds { get; set; } // En beräknande prop, om type och size > smth = NrOfBeds. Vad händer med price

    }
}
