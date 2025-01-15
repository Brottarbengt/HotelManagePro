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

        public void ShowStatusAllRooms()
        {
            var rooms = _roomService.GetAllRooms();
            foreach (var room in rooms)
            {
                Console.WriteLine($"Room {room.RoomNumber} - Type: {room.RoomType} - Is Active {room.IsActive}");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
        }

        public void ShowAvailableRooms()
        {
            Console.WriteLine("\nSelect dates to check room availability:");

            var (startDate, endDate) = GetDateRange();

            var availableRooms = _roomService.GetAvailableRooms(startDate, endDate);

            if (availableRooms.Count == 0)
            {
                Console.WriteLine($"\nNo available rooms found between {startDate:d} and {endDate:d}");
            }
            else
            {
                Console.WriteLine($"\nAvailable rooms between {startDate:d} and {endDate:d}:");
                DisplayRooms(availableRooms);
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(true);
        }

        private static (DateOnly startDate, DateOnly endDate) GetDateRange()
        {
            Console.WriteLine("\nSelect start date for availability check:");
            var startDate = DatePicker.PickDate("Start Date");

            while (true)
            {
                Console.WriteLine("\nSelect end date for availability check:");
                var endDate = DatePicker.PickDate("End Date");

                if (endDate > startDate)
                    return (startDate, endDate);

                Console.WriteLine("End date must be after start date. Please try again.");
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
                    DisplayRoom(room);
                }
                else
                {
                    Console.WriteLine("Room not found.");
                }
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
        }

        public void DisplayRooms(List<Room> rooms)
        {
            foreach (var room in rooms)
            {
                DisplayRoom(room);
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
        }

        public void DisplayRoom(Room room)
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

        public void EditRoomDetails()
        {
            var rooms = _roomService.GetAllRooms();
            var selectedRooms = RoomPicker.PickRooms(rooms);

            if (selectedRooms.Count == 0)
            {
                Console.WriteLine("No rooms selected.");
                
                return;
            }

            DisplayRooms(selectedRooms);

            foreach (var room in selectedRooms)
            {
                Console.WriteLine("Enter new details for the room (leave blank to keep current value):");

                Console.Write("Type (Single/Double): ");
                var newType = Console.ReadLine();
                if (!string.IsNullOrEmpty(newType) && Enum.TryParse(newType, out TypeOfRoom roomType))
                {
                    room.RoomType = roomType;
                }

                Console.Write("Size (m²): ");
                var newSize = Console.ReadLine();
                if (!string.IsNullOrEmpty(newSize) && int.TryParse(newSize, out int size))
                {
                    room.Size = size;
                }

                Console.Write("Price: ");
                var newPrice = Console.ReadLine();
                if (!string.IsNullOrEmpty(newPrice) && double.TryParse(newPrice, out double price))
                {
                    room.Price = price;
                }

                Console.Write("Status (Active/Inactive): ");
                var newStatus = Console.ReadLine();
                if (!string.IsNullOrEmpty(newStatus) && bool.TryParse(newStatus, out bool isActive))
                {
                    room.IsActive = isActive;
                }

                _roomService.UpdateRoom(room);
                Console.WriteLine("Room details updated successfully.");
            }
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(true);
        }

        public void CreateNewRoom()
        {
            Console.WriteLine("\nCreate New Room");
            
            Console.Write("Room Number: ");
            if (!int.TryParse(Console.ReadLine(), out int roomNumber))
            {
                Console.WriteLine("Invalid room number.");
                return;
            }

            if (_roomService.GetRoomByNumber(roomNumber) != null)
            {
                Console.WriteLine("Room number already exists.");
                return;
            }

            Console.Write("Room Type (Single/Double): ");
            if (!Enum.TryParse(Console.ReadLine(), out TypeOfRoom roomType))
            {
                Console.WriteLine("Invalid room type.");
                return;
            }

            Console.Write("Size (m²): ");
            if (!int.TryParse(Console.ReadLine(), out int size))
            {
                Console.WriteLine("Invalid size.");
                return;
            }

            Console.Write("Price: ");
            if (!double.TryParse(Console.ReadLine(), out double price))
            {
                Console.WriteLine("Invalid price.");
                return;
            }

            var room = new Room
            {
                RoomNumber = roomNumber,
                RoomType = roomType,
                Size = size,
                Price = price,
                IsActive = true
            };

            _roomService.CreateRoom(room);
            Console.WriteLine("Room created successfully!");
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(true);
        }

        public void DeleteRoom()
        {
            var rooms = _roomService.GetAllRooms();
            var selectedRooms = RoomPicker.PickRooms(rooms);

            if (selectedRooms.Count == 0)
            {
                Console.WriteLine("No rooms selected.");
                return;
            }

            foreach (var room in selectedRooms)
            {
                Console.WriteLine($"\nDelete Room {room.RoomNumber}?");
                DisplayRoom(room);
                Console.Write("Are you sure you want to delete this room? (y/n): ");
                if (Console.ReadLine()?.Trim().ToLower() == "y")
                {
                    _roomService.DeleteRoom(room);
                    Console.WriteLine("Room deleted successfully.");
                }
            }
            
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(true);
        }
    }
}
