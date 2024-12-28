using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagePro.Features.Customers.Models;

public class Address
{
    public int AddressId { get; set; }
    public required string StreetName { get; set; }
    public required string HouseNumber { get; set; }
    public required string City { get; set; }
    public required string PostalCode { get; set; } // Type String for scaling for implementation in foreign countries
    public Customer Customer { get; set; }
}
