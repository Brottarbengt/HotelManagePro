
namespace HotelManagePro.Features.Customers.Models;

public class Customer
{
    public int CustomerId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required DateOnly DateOfBirth { get; set; }
    public required int PhoneNumber { get; set; }
          
    public bool IsActive { get; set; }
    public Address? Address { get; set; }

}
