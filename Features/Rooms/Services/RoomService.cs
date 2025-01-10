using HotelManagePro.Database;
using HotelManagePro.Features.Rooms.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagePro.Features.Rooms.Services
{
    public class RoomService
    {
        private readonly ApplicationDbContext _dbContext;

        public RoomService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Room> GetAllRooms()
        {
            return _dbContext.Rooms.ToList();
        }

        
        public List<Room> GetAvailableRooms(DateOnly startDate, DateOnly endDate)
        {
            return _dbContext.Rooms
                .Where(r => r.IsActive)
                .Where(r => !_dbContext.Bookings
                    .Any(b => b.Rooms.Contains(r) && 
                             !(b.DepartureDate <= startDate || 
                               b.ArrivalDate >= endDate)))
                .ToList();
        }

        public Room? GetRoomByNumber(int roomNumber)
        {
            return _dbContext.Rooms.FirstOrDefault(r => r.RoomNumber == roomNumber);
        }

    }
}
