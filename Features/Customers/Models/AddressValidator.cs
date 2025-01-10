using FluentValidation;

namespace HotelManagePro.Features.Customers.Models;

public class AddressValidator : AbstractValidator<Address>
{
    public AddressValidator()
    {
        RuleFor(a => a.StreetName)
            .NotEmpty().WithMessage("Street name is required.")
            .Length(2, 100).WithMessage("Street name must be between 2 and 100 characters.");

        RuleFor(a => a.HouseNumber)
            .NotEmpty().WithMessage("House number is required.")
            .Length(1, 10).WithMessage("House number must be between 1 and 10 characters.")
            .Matches(@"^[0-9a-zA-Z-/\\]+$").WithMessage("House number can only contain numbers, letters, and -/\\");

        RuleFor(a => a.PostalCode)
            .NotEmpty().WithMessage("Postal code is required.")
            .Length(5, 10).WithMessage("Postal code must be between 5 and 10 characters.")
            .Matches(@"^[0-9\s-]+$").WithMessage("Postal code can only contain numbers, spaces and hyphens");

        RuleFor(a => a.City)
            .NotEmpty().WithMessage("City is required.")
            .Length(2, 50).WithMessage("City must be between 2 and 50 characters.")
            .Matches(@"^[a-zA-ZåäöÅÄÖ\s-]+$").WithMessage("City can only contain letters, spaces and hyphens");
    }

    public static bool ValidateAddress(string? streetName, string? houseNumber, string? postalCode, string? city)
    {
        var validator = new AddressValidator();
        var address = new Address
        {
            StreetName = streetName ?? "",
            HouseNumber = houseNumber ?? "",
            PostalCode = postalCode ?? "",
            City = city ?? ""
        };
        
        var result = validator.Validate(address);
        return result.IsValid;
    }
} 