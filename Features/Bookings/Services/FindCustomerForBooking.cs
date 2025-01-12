using HotelManagePro.Features.Customers.Controller;
using HotelManagePro.Features.Customers.Services;
using System;

namespace HotelManagePro.Features.Bookings.Services;

public class FindCustomerForBooking
{
    private readonly CustomerController _customerController;
    private readonly CustomerService _customerService;
    public int? SelectedCustomerId { get; private set; }

    public FindCustomerForBooking(CustomerController customerController, CustomerService customerService)
    {
        _customerController = customerController;
        _customerService = customerService;
    }

    public int? FindByEmail()
    {
        while (true)
        {
            Console.WriteLine("\nPress ESC to return to menu or enter customer email:");
            
            while (Console.KeyAvailable) Console.ReadKey(true);
            
            var key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Escape)
                return null;

            Console.Write("Email: ");
            var email = Console.ReadLine();
            
            var customer = _customerService.GetCustomerByEmail(email);
            if (customer != null)
            {
                _customerController.DisplayCustomer(customer);
                Console.Write("\nAdd this customer to booking? (y/n): ");
                if (Console.ReadLine()?.Trim().ToLower() == "y")
                {
                    return customer.CustomerId;
                }
            }
            else
            {
                Console.WriteLine("Customer not found.");
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey(true);
            }
        }
    }

    public int? FindByPhone()
    {
        while (true)
        {
            Console.WriteLine("\nPress ESC to return to menu or enter phone number:");
            
            while (Console.KeyAvailable) Console.ReadKey(true);
            
            var key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Escape)
                return null;

            Console.Write("Phone: ");
            var phoneInput = Console.ReadLine();
            
            if (int.TryParse(phoneInput, out int phoneNumber))
            {
                var customer = _customerService.GetCustomerByPhone(phoneNumber);
                if (customer != null)
                {
                    _customerController.DisplayCustomer(customer);
                    Console.Write("\nAdd this customer to booking? (y/n): ");
                    if (Console.ReadLine()?.Trim().ToLower() == "y")
                    {
                        return customer.CustomerId;
                    }
                }
                else
                {
                    Console.WriteLine("Customer not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid phone number format.");
            }
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(true);
        }
    }

    public int? FindById()
    {
        Console.Write("\nEnter Customer ID: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            var customer = _customerService.GetCustomerById(id);
            if (customer != null)
            {
                _customerController.DisplayCustomer(customer);
                Console.Write("\nAdd this customer to booking? (y/n): ");
                if (Console.ReadLine()?.Trim().ToLower() == "y")
                {
                    return id;
                }
            }
            else
            {
                Console.WriteLine("Customer not found.");
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey(true);
            }
        }
        else
        {
            Console.WriteLine("Invalid ID format.");
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(true);
        }
        return null;
    }
} 