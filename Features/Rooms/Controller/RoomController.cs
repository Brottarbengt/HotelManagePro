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
        private readonly BookingService _bookingService;

        public RoomController(RoomService roomService, BookingService bookingService)
        {
            _roomService = roomService;
            _bookingService = bookingService;
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
                    Console.WriteLine($"Active: {room.IsActive}");
                }
                else
                {
                    Console.WriteLine("Room not found.");
                }
            }
        }

        public void DisplayRoom(List<Room> rooms)
        {
            foreach (var room in rooms)
            {
                Console.WriteLine($"\nRoom {room.RoomNumber}");
                Console.WriteLine($"Type: {room.RoomType}");
                Console.WriteLine($"Size: {room.Size} m²");
                Console.WriteLine($"Price: {room.Price:C}");
                Console.WriteLine($"Status: {(room.IsActive ? "Active" : "Inactive")}");

                var bookedDates = _bookingService.GetRoomBookedDates(room.RoomNumber);
                if (bookedDates.Count != 0)
                {
                    Console.WriteLine("Booked Dates:");
                    foreach (var date in bookedDates)
                    {
                        Console.WriteLine(date.ToString("yyyy-MM-dd"));
                    }
                }
                else
                {
                    Console.WriteLine("No future bookings.");
                }
            }
        }

        public void EditRoomDetails()
        {
            var rooms = _roomService.GetAllRooms();
            var selectedRooms = RoomPicker.PickRooms(rooms);
            
            if (selectedRooms.Count == 0)
            {
                Console.WriteLine("No rooms selected.");
                return;
            }
            else
            DisplayRoom(selectedRooms);


        }
    }
}
