using FluentValidation;

namespace HotelManagePro.Features.Customers.Models;

public class CustomerValidator : AbstractValidator<Customer>
{
    public CustomerValidator()
    {
        RuleFor(c => c.FirstName)
            .NotEmpty().WithMessage("Name is required.")
            .MinimumLength(2).WithMessage("Name must be at least 2 characters.")
            .Matches(@"^[a-zA-ZåäöÅÄÖ]+$").WithMessage("Name can only contain letters.");

        RuleFor(c => c.LastName)
            .NotEmpty().WithMessage("Name is required.")
            .MinimumLength(2).WithMessage("Name must be at least 2 characters.")
            .Matches(@"^[a-zA-ZåäöÅÄÖ]+$").WithMessage("Name can only contain letters.");


        RuleFor(c => c.Email)
           .NotEmpty().WithMessage("Email is required.")
           .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$").WithMessage("Invalid email format.");

        RuleFor(c => c.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .GreaterThan(0).WithMessage("Phone number must be greater than 0")
            .Must(p => p.ToString().StartsWith("0")).WithMessage("Phone number must begin with 0")
            .Must(p => p.ToString().Length >= 6 && p.ToString().Length <= 15)
                .WithMessage("Phone number must be between 6 and 15 digits");

        RuleFor(c => c.DateOfBirth)
           .NotEmpty().WithMessage("Date of Birth is required.")
           .Must(BeAtLeast18YearsOld).WithMessage("Customer must be at least 18 years old.");

        RuleFor(c => c.Address)
            .SetValidator(new AddressValidator()!)
            .When(c => c.Address != null);
    }
    private bool BeAtLeast18YearsOld(DateOnly dateOfBirth)
    {
        var today = DateOnly.FromDateTime(DateTime.Now);
        var age = today.Year - dateOfBirth.Year;
        if (dateOfBirth > today.AddYears(-age)) age--;
        return age >= 18;
    }

    public static bool IsValidEmail(string? email)
    {
        var validator = new CustomerValidator();
        var customer = new Customer 
        { 
            FirstName = "Temp",
            LastName = "Temp",
            Email = email ?? "",
            PhoneNumber = 0,
            DateOfBirth = DateOnly.FromDateTime(DateTime.Now),
            Address = new Address
            {
                StreetName = "Temp",
                HouseNumber = "1",
                PostalCode = "12345",
                City = "TempCity"
            }
        };
        var result = validator.Validate(customer);
        return result.IsValid;
    } 

    public static bool IsValidName(string? name)
    {
        return !string.IsNullOrWhiteSpace(name) && name.Length >= 2 && name.Length <= 50;
    }

    public static bool IsValidPhoneNumber(string? input, out int phoneNumber)
    {
        phoneNumber = 0;
        if (string.IsNullOrWhiteSpace(input)) return false;
        return int.TryParse(input, out phoneNumber) && input.Length >= 8 && input.Length <= 15;
    }

    public static bool IsValidDateOfBirth(string? input, out DateOnly dob)
    {
        dob = default;
        if (string.IsNullOrWhiteSpace(input)) return false;
        
        if (!DateOnly.TryParse(input, out dob)) return false;
        
        var minAge = 18;
        var maxAge = 120;
        var today = DateOnly.FromDateTime(DateTime.Today);
        var age = today.Year - dob.Year;
        
        if (dob > today.AddYears(-age)) age--;
        
        return age >= minAge && age <= maxAge;
    }

    public static bool IsValidAddress(string? streetName, string? houseNumber, string? postalCode, string? city)
    {
        return AddressValidator.ValidateAddress(streetName, houseNumber, postalCode, city);
    }

    public static string? GetValidatedEmail()
    {
        while (true)
        {
            Console.Write("Enter email: ");
            var email = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(email))
            {
                Console.WriteLine("Email cannot be empty. Please try again.");
                continue;
            }

            if (!email.Contains('@'))
            {
                Console.WriteLine("Email must contain '@'. Please try again.");
                continue;
            }

            var parts = email.Split('@');
            if (parts.Length != 2)
            {
                Console.WriteLine("Invalid email format. Please try again.");
                continue;
            }

            var localPart = parts[0];
            var domainPart = parts[1];

            if (string.IsNullOrWhiteSpace(localPart) || string.IsNullOrWhiteSpace(domainPart))
            {
                Console.WriteLine("Invalid email format. Please try again.");
                continue;
            }

            if (!domainPart.Contains('.'))
            {
                Console.WriteLine("Invalid domain format. Please try again.");
                continue;
            }

            if (email.Length > 254)
            {
                Console.WriteLine("Email is too long. Please try again.");
                continue;
            }

            return email;
        }
    }
}