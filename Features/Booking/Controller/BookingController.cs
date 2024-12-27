using HotelManagePro.Database;
using HotelManagePro.Features.Booking.Models;
using HotelManagePro.Features.Booking.Services;
using HotelManagePro.Features.Invoice.Models;
using HotelManagePro.Features.Room.Models;
using HotelManagePro.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagePro.Features.Booking.Controller
{
    public class BookingController
    {
        private readonly BookingService _bookingService;

        public BookingController(BookingService bookingService)
        {
            _bookingService = bookingService;
        }
        public void CreateNewBooking()   // Den här är bara skit
        {

            //  New Booking -> Väljer Datum -> Visar lediga rum, väljer rum -> Ta Personuppgifter -> Bekräftar booking
            var arrivalDate = DatePicker.PickDate();
            var departureDate = DatePicker.PickDate();

            // var rooms = RoomPicker(Dates) -> ShowAvailableRooms(Dates) -> Return rooms


            //var customer = CustomerInput() -> SearchOnEmail() -> if true -> ShowCustomerInfo() for validation,
            //Is info correct? yes/No -> EditCustomer() or
            //ConfirmBooking() else: InputNewCustomer()

            //Var CreateInvoice()

            var newBooking = new Bookings
            {
                
            };
            
            // save to database

        }

        public void EditBooking(int bookingId)
        {
            
        }

        public void DisplayBooking(Bookings booking)
        {
            Console.WriteLine($"Booking ID: {booking.BookingsId}");
            Console.WriteLine($"Customer: {booking.Customers.FirstName} {booking.Customers.LastName}");
            Console.WriteLine($"Arrival Date: {booking.ArrivalDate}");
            Console.WriteLine($"Departure Date: {booking.DepartureDate}");
            Console.WriteLine($"Rooms: {string.Join(", ", booking.Rooms.Select(r => $"Room {r.RoomNumber} ({r.RoomType})"))}");
            Console.WriteLine($"Is Paid: {booking.Invoice?.IsPaid ?? false}");
            Console.WriteLine($"Total Cost: {booking.Invoice?.TotalSum ?? 0}");
            Console.WriteLine(new string('-', 40));
        }
    }
}
