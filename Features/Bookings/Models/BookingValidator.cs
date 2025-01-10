using FluentValidation;
using HotelManagePro.Features.Customers.Models;
using HotelManagePro.Features.Invoices.Models;
using HotelManagePro.Features.Rooms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            // Invoice validation
            RuleFor(b => b.Invoice)
                .NotNull().WithMessage("Invoice is required.")
                .SetValidator(new InvoiceValidator());

            // Customer validation
            RuleFor(b => b.Customer)
                .NotNull().WithMessage("Customer information is required.")
                .SetValidator(new CustomerValidator());

            RuleFor(b => b.Rooms)
                .NotNull().WithMessage("At least one room must be selected.")
                .Must(rooms => rooms.Any()).WithMessage("Rooms list cannot be empty.")
                .Must(rooms => rooms.All(r => r.IsActive))
                .WithMessage("All selected rooms must be active.");

            RuleFor(b => b.Rooms)
                .Must((booking, rooms) => RoomsAreAvailable(rooms, booking.ArrivalDate, booking.DepartureDate))
                .WithMessage("One or more selected rooms are already booked for the chosen dates.");
        }

        public static int GetValidBookingId(string? input, List<Booking>? bookings)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentException("Booking ID cannot be empty.");
        
            if (bookings == null || !bookings.Any())
                throw new ArgumentException("No bookings available.");

            if (!int.TryParse(input, out int bookingId))
                throw new ArgumentException("Invalid booking ID format.");

            if (!bookings.Any(b => b.BookingId == bookingId))
                throw new ArgumentException($"No booking found with ID {bookingId}.");

            return bookingId;
        }

        private bool RoomsAreAvailable(List<Room> rooms, DateOnly arrivalDate, DateOnly departureDate)
        {
            // Replace with logic to check room availability.
            
            return true;
        }
    }
}
