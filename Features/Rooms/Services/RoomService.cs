using HotelManagePro.Database;
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

        public List<Rooms> RoomPicker(DateOnly arrivalDate, DateOnly departureDate)
        {
            //first Show available rooms


            var room = 
            return room;
        }
    }
}
