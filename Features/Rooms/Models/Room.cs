using HotelManagePro.Features.Bookings.Models;


namespace HotelManagePro.Features.Rooms.Models
{
    public enum TypeOfRoom
    {
        Single,
        Double
    }
    public class Room
    {
        public int RoomsId { get; set; }        
        public required int RoomNumber { get; set; }
        public required TypeOfRoom RoomType { get; set; }
        public required int Size { get; set; }
        public required bool IsActive { get; set; }
        public required double Price { get; set; }

    }    
}
