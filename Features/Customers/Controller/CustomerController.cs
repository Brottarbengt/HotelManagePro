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

    public Customer CreateNewCustomer()
    {
        while (true)
        {
            try
            {
                Console.WriteLine("\nEnter customer details:");
                
                Console.Write("First Name: ");
                var firstName = Console.ReadLine();
                if (!CustomerValidator.IsValidName(firstName))
                    throw new ArgumentException("Invalid first name");

                Console.Write("Last Name: ");
                var lastName = Console.ReadLine();
                if (!CustomerValidator.IsValidName(lastName))
                    throw new ArgumentException("Invalid last name");

                Console.Write("Email: ");
                var email = Console.ReadLine();
                if (!CustomerValidator.IsValidEmail(email))
                    throw new ArgumentException("Invalid email format");

                Console.Write("Phone Number: ");
                var phoneInput = Console.ReadLine();
                if (!CustomerValidator.IsValidPhoneNumber(phoneInput, out int phoneNumber))
                    throw new ArgumentException("Invalid phone number");

                Console.Write("Date of Birth (YYYY-MM-DD): ");
                var dobInput = Console.ReadLine();
                if (!CustomerValidator.IsValidDateOfBirth(dobInput, out DateOnly dob))
                    throw new ArgumentException("Invalid date of birth");

                // Address details
                Console.Write("Street Name: ");
                var streetName = Console.ReadLine();
                
                Console.Write("House Number: ");
                var houseNumber = Console.ReadLine();
                
                Console.Write("Postal Code: ");
                var postalCode = Console.ReadLine();
                
                Console.Write("City: ");
                var city = Console.ReadLine();

                if (!CustomerValidator.IsValidAddress(streetName, houseNumber, postalCode, city))
                    throw new ArgumentException("Invalid address details");

                var customer = new Customer
                {
                    FirstName = firstName!,
                    LastName = lastName!,
                    Email = email!,
                    PhoneNumber = phoneNumber,
                    DateOfBirth = dob,
                    IsActive = true,
                    Address = new Address
                    {
                        StreetName = streetName!,
                        HouseNumber = houseNumber!,
                        PostalCode = postalCode!,
                        City = city!
                    }
                };

                return _customerService.CreateNewCustomer(customer);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}. Please try again.");
            }
        }
    }

    public Customer? FindByEmailCustomer()
    {
        Console.Write("Enter email: ");
        var email = Console.ReadLine();
        return _customerService.GetCustomerByEmail(email);
    }

    public void FindCustomerById()
    {
        Console.Write("Enter customer ID: ");
        if (int.TryParse(Console.ReadLine(), out int id))
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
    }

    public void FindCustomerByEmail()
    {
        Console.Write("Enter email: ");
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

    public void FindCustomerByAddress()
    {
        Console.Write("Enter address: ");
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

    public void ShowAllCustomers()
    {
        var customers = _customerService.GetAllCustomers();
        if (!customers.Any())
        {
            Console.WriteLine("No customers found.");
            return;
        }

        foreach (var customer in customers)
        {
            DisplayCustomer(customer);
        }
    }

    private void DisplayCustomer(Customer customer)
    {
        Console.WriteLine($"ID: {customer.CustomerId}");
        Console.WriteLine($"Name: {customer.FirstName} {customer.LastName}");
        Console.WriteLine($"Email: {customer.Email}");
        Console.WriteLine($"Phone: {customer.PhoneNumber}");
        if (customer.Address != null)
        {
            Console.WriteLine($"Address: {customer.Address.StreetName}, {customer.Address.HouseNumber}");
            Console.WriteLine($"Postal Code: {customer.Address.PostalCode}, {customer.Address.City}");
            
        }   
        Console.WriteLine(new string('-', 40));
    }
}
