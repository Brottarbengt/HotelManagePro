using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagePro.Features.Customer.Models
{
    public class Address
    {
        public string? StreetName { get; set; }
        public string? HouseNumber { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; } // String for scaling to other countries
    }
}
