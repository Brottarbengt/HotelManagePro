using HotelManagePro.Features.Bookings.Services;
using HotelManagePro.Features.Rooms.Models;
using HotelManagePro.Features.Rooms.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManagePro.Utils;

namespace HotelManagePro.Features.Rooms.Controller
{
    public class RoomController
    {
        private readonly RoomService _roomService;

        public RoomController(RoomService roomService)
        {
            _roomService = roomService;
        }

        public void ShowAllRooms()
        {
            var rooms = _roomService.GetAllRooms();
            foreach (var room in rooms)
            {
                Console.WriteLine($"Room {room.RoomNumber} - Type: {room.RoomType}");
            }
        }

        public void ShowAvailableRooms()
        {
            var availableRooms = _roomService.GetAvailableRooms(DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now));
            foreach (var room in availableRooms)
            {
                Console.WriteLine($"Room {room.RoomNumber} - Type: {room.RoomType}");
            }
        }

        public void ShowRoomDetails()
        {
            Console.Write("Enter room number: ");
            if (int.TryParse(Console.ReadLine(), out int roomNumber))
            {
                var room = _roomService.GetRoomByNumber(roomNumber);
                if (room != null)
                {
                    Console.WriteLine($"Room {room.RoomNumber}");
                    Console.WriteLine($"Type: {room.RoomType}");
                    Console.WriteLine($"Price: {room.Price:C}");
                }
                else
                {
                    Console.WriteLine("Room not found.");
                }
            }
        }

        public void EditRoomDetails()
        {
            var rooms = _roomService.GetAllRooms();
            var selectedRooms = RoomPicker.PickRooms(rooms);
            
            if (!selectedRooms.Any())
            {
                Console.WriteLine("No rooms selected.");
                return;
            }

            foreach (var room in selectedRooms)
            {
                Console.WriteLine($"\nEditing Room {room.RoomNumber}");
                Console.WriteLine($"Current details:");
                Console.WriteLine($"Type: {room.RoomType}");
                Console.WriteLine($"Size: {room.Size} m²");
                Console.WriteLine($"Price: {room.Price:C}");
                Console.WriteLine($"Status: {(room.IsActive ? "Active" : "Inactive")}");
            }
        }
    }
}
