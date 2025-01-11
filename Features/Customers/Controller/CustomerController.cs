using HotelManagePro.Features.Bookings.Services;
using HotelManagePro.Features.Customers.Services;
using HotelManagePro.Features.Customers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagePro.Features.Customers.Controller;

public class CustomerController
{
    private readonly CustomerService _customerService;

    public CustomerController(CustomerService customerService)
    {
        _customerService = customerService;
    }

    public Customer? CreateNewCustomer()
    {
        while (true)
        {
            try
            {
                Console.WriteLine("\nPress ESC at any time to cancel.");

                // First Name
                Console.Write("First Name: ");
                var firstName = GetInput();
                if (firstName == null) return null;
                if (!CustomerValidator.IsValidName(firstName))
                    throw new ArgumentException("Invalid first name");

                // Last Name
                Console.Write("Last Name: ");
                var lastName = GetInput();
                if (lastName == null) return null;
                if (!CustomerValidator.IsValidName(lastName))
                    throw new ArgumentException("Invalid last name");

                // Email
                Console.Write("Email: ");
                var email = GetInput();
                if (email == null) return null;
                if (!CustomerValidator.IsValidEmail(email))
                    throw new ArgumentException("Invalid email format");

                // Phone
                Console.Write("Phone Number: ");
                var phoneInput = GetInput();
                if (phoneInput == null) return null;
                if (!CustomerValidator.IsValidPhoneNumber(phoneInput, out int phoneNumber))
                    throw new ArgumentException("Invalid phone number");

                // Date of Birth
                Console.Write("Date of Birth (YYYY-MM-DD): ");
                var dobInput = GetInput();
                if (dobInput == null) return null;
                if (!CustomerValidator.IsValidDateOfBirth(dobInput, out DateOnly dob))
                    throw new ArgumentException("Invalid date of birth");

                // Address details
                Console.Write("Add Customer Address? (y/n): ");
                var addAddress = GetInput()?.ToLower();
                if (addAddress == null) return null;

                Address? address = null;
                if (addAddress == "y")
                {
                    Console.Write("Street Name: ");
                    var streetName = GetInput();
                    if (streetName == null) return null;

                    Console.Write("House Number: ");
                    var houseNumber = GetInput();
                    if (houseNumber == null) return null;

                    Console.Write("Postal Code: ");
                    var postalCode = GetInput();
                    if (postalCode == null) return null;

                    Console.Write("City: ");
                    var city = GetInput();
                    if (city == null) return null;

                    if (!CustomerValidator.IsValidAddress(streetName, houseNumber, postalCode, city))
                        throw new ArgumentException("Invalid address details");

                    address = new Address
                    {
                        StreetName = streetName,
                        HouseNumber = houseNumber,
                        PostalCode = postalCode,
                        City = city
                    };
                }

                var customer = new Customer
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    PhoneNumber = phoneNumber,
                    DateOfBirth = dob,
                    IsActive = true,
                    Address = address
                };

                return _customerService.CreateNewCustomer(customer);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}. Please try again or press ESC to cancel.");
            }
        }
    }

    private string? GetInput()
    {
        while (true)
        {
            while (Console.KeyAvailable) Console.ReadKey(true);

            var input = "";
            ConsoleKeyInfo key;

            while ((key = Console.ReadKey(true)).Key != ConsoleKey.Enter)
            {
                if (key.Key == ConsoleKey.Escape)
                    return null;

                if (key.Key == ConsoleKey.Backspace && input.Length > 0)
                {
                    input = input[..^1];
                    Console.Write("\b \b");
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    input += key.KeyChar;
                    Console.Write(key.KeyChar);
                }
            }
            Console.WriteLine();
            return input;
        }
    }

    public void FindCustomerById()
    {
        while (true)
        {
            Console.WriteLine("\nPress ESC to return to menu or enter customer ID:");
            
            while (Console.KeyAvailable) Console.ReadKey(true); // Clear key buffer
            
            var key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Escape)
                return;

            Console.Write("ID: ");
            var input = Console.ReadLine();
            
            if (int.TryParse(input, out int id))
            {
                var customer = _customerService.GetCustomerById(id);
                if (customer != null)
                {
                    DisplayCustomer(customer);
                }
                else
                {
                    Console.WriteLine("Customer not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID format.");
            }
        }
    }

    public void FindCustomerByEmail()
    {
        while (true)
        {
            Console.WriteLine("\nPress ESC to return to menu or enter customer email:");
            
            while (Console.KeyAvailable) Console.ReadKey(true);
            
            var key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Escape)
                return;

            Console.Write("Email: ");
            var email = Console.ReadLine();
            
            var customer = _customerService.GetCustomerByEmail(email);
            if (customer != null)
            {
                DisplayCustomer(customer);
            }
            else
            {
                Console.WriteLine("Customer not found.");
            }
        }
    }

    public void FindCustomerByAddress()
    {
        while (true)
        {
            Console.WriteLine("\nPress ESC to return to menu or enter address to search:");
            
            while (Console.KeyAvailable) Console.ReadKey(true);
            
            var key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Escape)
                return;

            Console.Write("Address: ");
            var address = Console.ReadLine();
            
            var customers = _customerService.GetCustomersByAddress(address);
            if (customers.Any())
            {
                foreach (var customer in customers)
                {
                    DisplayCustomer(customer);
                }
            }
            else
            {
                Console.WriteLine("No customers found.");
            }
        }
    }

    public void ShowAllCustomers()
    {
        var customers = _customerService.GetAllCustomers();
        if (!customers.Any())
        {
            Console.WriteLine("No customers found.");
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(true);
            return;
        }

        foreach (var customer in customers)
        {
            DisplayCustomer(customer);
        }
        
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey(true);
    }

    private void DisplayCustomer(Customer customer)
    {
        Console.WriteLine($"ID: {customer.CustomerId}");
        Console.WriteLine($"Name: {customer.FirstName} {customer.LastName}");
        Console.WriteLine($"Email: {customer.Email}");
        Console.WriteLine($"Phone: {customer.PhoneNumber}");
        Console.WriteLine($"Date of Birth: {customer.DateOfBirth:d}");
        Console.WriteLine($"Status: {(customer.IsActive ? "Active" : "Inactive")}");
        
        if (customer.Address != null)
        {
            Console.WriteLine($"Address: {customer.Address.StreetName} {customer.Address.HouseNumber}");
            Console.WriteLine($"         {customer.Address.PostalCode} {customer.Address.City}");
        }
        else
        {
            Console.WriteLine("No address registered");
        }
        
        Console.WriteLine(new string('-', 40));
    }
}
