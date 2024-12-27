using HotelManagePro.Features.Booking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagePro.Features.Customer.Models
{
    public class Customers
    {
        public int CustomersId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required int PhoneNumber { get; set; }
        public List<Bookings> Bookings { get; set; }
        public string? StreetName { get; set; }
        public string? HouseNumber { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; } // String for scaling to other countries
        public bool IsActive { get; set; }

    }
}
