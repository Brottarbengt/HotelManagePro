using FluentValidation;
using HotelManagePro.Features.Customers.Models;
using HotelManagePro.Features.Invoices.Models;


namespace HotelManagePro.Features.Bookings.Models
{
    public class BookingValidator : AbstractValidator<Booking>
    {
        public BookingValidator()
        {
            
            RuleFor(b => b.ArrivalDate)
                .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now))
                .WithMessage("Arrival date must be today or in the future.");

            RuleFor(b => b.DepartureDate)
                .GreaterThan(b => b.ArrivalDate)
                .WithMessage("Departure date must be after the arrival date.");

            // Invoices validation
            RuleFor(b => b.Invoice)
                .NotNull().WithMessage("Invoice is required.")
                .SetValidator(new InvoiceValidator());

            // Customers validation
            RuleFor(b => b.Customer)
                .NotNull().WithMessage("Customer information is required.")
                .SetValidator(new CustomerValidator());

            RuleFor(b => b.Rooms)
                .NotNull().WithMessage("At least one room must be selected.")
                .Must(rooms => rooms.Count != 0).WithMessage("Rooms list cannot be empty.")
                .Must(rooms => rooms.All(r => r.IsActive))
                .WithMessage("All selected rooms must be active.");

            
        }

        public int GetValidBookingId(string? input, List<Booking>? bookings)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentException("Booking ID cannot be empty.");
        
            if (bookings == null || bookings.Count == 0)
                throw new ArgumentException("No bookings available.");

            if (!int.TryParse(input, out int bookingId))
                throw new ArgumentException("Invalid booking ID format.");

            if (!bookings.Any(b => b.BookingId == bookingId))
                throw new ArgumentException($"No booking found with ID {bookingId}.");

            return bookingId;
        }

        
    }
}
