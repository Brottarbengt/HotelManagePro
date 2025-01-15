using HotelManagePro.Features.Customers.Services;
using HotelManagePro.Features.Customers.Models;
using Spectre.Console;

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

    private static string? GetInput()
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

    public void FindCustomerByPhone()
    {
        while (true)
        {
            Console.WriteLine("\nPress ESC to return to menu or enter phone number:");
            
            while (Console.KeyAvailable) Console.ReadKey(true);
            
            var key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Escape)
                return;

            Console.Write("Phone number: ");
            var phoneInput = Console.ReadLine();
            
            if (int.TryParse(phoneInput, out int phoneNumber))
            {
                var customer = _customerService.GetCustomerByPhone(phoneNumber);
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
                Console.WriteLine("Invalid phone number format.");
            }
        }
    }

    public void ShowAllCustomers()
    {
        var customers = _customerService.GetAllCustomers();
        if (customers.Count == 0)
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

    public void DisplayCustomer(Customer customer)
    {
        Console.WriteLine($"ID: {customer.CustomerId}");
        Console.WriteLine($"Name: {customer.FirstName} {customer.LastName}");
        Console.WriteLine($"Email: {customer.Email}");
        Console.WriteLine($"Phone: {customer.PhoneNumber}");
        Console.WriteLine($"Date of Birth: {customer.DateOfBirth:d}");
        Console.WriteLine($"Status: {(customer.IsActive ? "Active" : "Soft Deleted")}");
        
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

    private Customer? FindCustomer(string purpose)
    {
        while (true)
        {
            Console.Clear();
            AnsiConsole.MarkupLine($"\n[blue]Find Customer to {purpose}[/]");
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("How would you like to find the customer?")
                    .AddChoices(
                    [
                        "Find by ID",
                        "Find by Email",
                        "Find by Phone",
                        "Back to Menu"
                    ]));

            Customer? customer = null;
            switch (choice)
            {
                case "Find by ID":
                    Console.Write("\nEnter Customer ID: ");
                    if (int.TryParse(Console.ReadLine(), out int id))
                    {
                        customer = _customerService.GetCustomerById(id);
                    }
                    break;

                case "Find by Email":
                    Console.Write("\nEnter Email: ");
                    var email = Console.ReadLine();
                    customer = _customerService.GetCustomerByEmail(email);
                    break;

                case "Find by Phone":
                    Console.Write("\nEnter Phone: ");
                    if (int.TryParse(Console.ReadLine(), out int phone))
                    {
                        customer = _customerService.GetCustomerByPhone(phone);
                    }
                    break;

                case "Back to Menu":
                    return null;
            }

            if (customer != null)
            {
                Console.WriteLine("\nCustomer found:");
                DisplayCustomer(customer);
                return customer;
            }
            
            Console.WriteLine("Customer not found.");
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(true);
        }
    }

    public void FindCustomerForUpdate()
    {
        var customer = FindCustomer("Update");
        if (customer != null)
        {
            UpdateCustomerDetails(customer);
        }
    }

    public void SoftDeleteCustomer()
    {
        var customer = FindCustomer("Soft Delete");
        if (customer != null)
        {
            Console.Write("\nAre you sure you want to soft delete this customer? (y/n): ");
            if (Console.ReadLine()?.Trim().ToLower() == "y")
            {
                customer.IsActive = false;
                _customerService.UpdateCustomer(customer);
                Console.WriteLine("Customer soft deleted successfully.");
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey(true);
            }
        }
    }

    public void HardDeleteCustomer()
    {
        var customer = FindCustomer("Hard Delete");
        if (customer != null)
        {
            Console.WriteLine("\n[red]WARNING: This action cannot be undone![/]");
            Console.Write("Are you sure you want to permanently delete this customer? (y/n): ");
            if (Console.ReadLine()?.Trim().ToLower() == "y")
            {
                _customerService.DeleteCustomer(customer);
                Console.WriteLine("Customer permanently deleted.");
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey(true);
            }
        }
    }

    private void UpdateCustomerDetails(Customer customer)
    {
        Console.WriteLine("\nEnter new details (press Enter to keep current value):");

        Console.Write($"First Name ({customer.FirstName}): ");
        var firstName = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(firstName) && CustomerValidator.IsValidName(firstName))
            customer.FirstName = firstName;

        Console.Write($"Last Name ({customer.LastName}): ");
        var lastName = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(lastName) && CustomerValidator.IsValidName(lastName))
            customer.LastName = lastName;

        Console.Write($"Email ({customer.Email}): ");
        var email = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(email) && CustomerValidator.IsValidEmail(email))
            customer.Email = email;

        Console.Write($"Phone ({customer.PhoneNumber}): ");
        var phoneInput = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(phoneInput) && CustomerValidator.IsValidPhoneNumber(phoneInput, out int phone))
            customer.PhoneNumber = phone;

        Console.Write($"Date of Birth ({customer.DateOfBirth:yyyy-MM-dd}): ");
        var dobInput = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(dobInput) && CustomerValidator.IsValidDateOfBirth(dobInput, out DateOnly dob))
            customer.DateOfBirth = dob;

        Console.Write("Update address? (y/n): ");
        if (Console.ReadLine()?.ToLower() == "y")
        {
            Console.Write($"Street Name ({customer.Address?.StreetName}): ");
            var streetName = Console.ReadLine();

            Console.Write($"House Number ({customer.Address?.HouseNumber}): ");
            var houseNumber = Console.ReadLine();

            Console.Write($"Postal Code ({customer.Address?.PostalCode}): ");
            var postalCode = Console.ReadLine();

            Console.Write($"City ({customer.Address?.City}): ");
            var city = Console.ReadLine();

            if (CustomerValidator.IsValidAddress(streetName, houseNumber, postalCode, city))
            {
                customer.Address = new Address
                {
                    StreetName = streetName!,
                    HouseNumber = houseNumber!,
                    PostalCode = postalCode!,
                    City = city!
                };
            }
        }

        _customerService.UpdateCustomer(customer);
        Console.WriteLine("\nCustomer updated successfully!");
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey(true);
    }
}
