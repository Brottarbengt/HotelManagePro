using FluentValidation;
using FluentValidation.Validators;
using HotelManagePro.Features.Bookings.Models;

namespace HotelManagePro.Features.Invoices.Models;

internal class InvoiceValidator : AbstractValidator<Invoice>
{
    public InvoiceValidator()
    {
        RuleFor(i => i.TotalSum)
            .GreaterThan(0).WithMessage("Total sum must be greater than 0.");

        RuleFor(i => i.IsPaid)
            .NotNull().WithMessage("Payment status must be specified.");

        RuleFor(i => i.ExtraBeds)
            .GreaterThanOrEqualTo(0).WithMessage("Extra beds cannot be negative.");
    }
}