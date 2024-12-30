using HotelManagePro.Features.Bookings.Services;
using HotelManagePro.Features.Rooms.Models;
using HotelManagePro.Features.Rooms.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagePro.Features.Rooms.Controller
{
    public class RoomController
    {
        private readonly RoomService _roomService;

        public RoomController(RoomService roomService)
        {
            _roomService = roomService;
        }
        public List<Room> RoomPicker(DateOnly arrivalDate, DateOnly departureDate)
        {
            //first Show available rooms


            var room =
            return room;
        }

    }
}
