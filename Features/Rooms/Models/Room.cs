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

        public static Room CreateSingleRoom(int roomNumber){
            return new Room
            {
                RoomNumber = roomNumber,
                RoomType = TypeOfRoom.Single,
                Size = 10,
                IsActive = true,
                Price = 650   

            };
        }

        public static Room CreateDoubleRoom(int roomNumber){
            return new Room
            {
                RoomNumber = roomNumber,
                RoomType = TypeOfRoom.Double,
                Size = 20,
                IsActive = true,
                Price = 1200
            };
        }
    }
    
}
