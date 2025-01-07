using HotelManagePro.Features.Bookings.Services;
using HotelManagePro.Features.Customers.Services;
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

    public Customer FindByEmailCustomer()
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

    private void DisplayCustomer(Customer customer)
    {
        Console.WriteLine($"ID: {customer.CustomersId}");
        Console.WriteLine($"Name: {customer.FirstName} {customer.LastName}");
        Console.WriteLine($"Email: {customer.Email}");
        Console.WriteLine($"Phone: {customer.PhoneNumber}");
        if (customer.Address != null)
        {
            Console.WriteLine($"Address: {customer.Address.Street}, {customer.Address.City}");
        }
        Console.WriteLine(new string('-', 40));
    }
}
