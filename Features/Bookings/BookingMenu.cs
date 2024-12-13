using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagePro.Features.Bookings
{
    public static class BookingMenu
    {
        
        private static string[] menuOptions = new string[] {            
            "New Booking",
            "Search for Booking", // Undermeny med vad man vill söka på? alt. söker igenom alla attribut på något som stämmer?
            "Show All Bookings",
            "Remove Booking", // Ska ShowAllBookings en månad framöver ovan prompt
            "Edit Booking", // Ska ShowAllBookings en månad framöver ovan prompt
            "Back to Top Menu"
        };
    }
}
