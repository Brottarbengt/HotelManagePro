using HotelManagePro.Features.Booking.Services;
using HotelManagePro.Utils.Menu;

namespace HotelManagePro.Features.Booking.Menus;

public static class SearchBookingMenu
{
    public static void ShowMenu()
    {
        string[] options = new[]
        {
            "Edit Booking",
            "Remove Booking",
            "Back to Booking Menu"
        };

        MenuGenerator.ShowMenu("Search Bookings", options, ExecuteOption);
    }

    private static BookingService _bookingService;
    public static void SetBookingService(BookingService bookingService)
    {
        _bookingService = bookingService;
    }



    private static void ExecuteOption(int selectedIndex)
    {
        switch (selectedIndex)
        {
            case 0:
                _bookingService.EditBooking();
                break;
            case 1:
                _bookingService.RemoveBooking();
                break;
            case 2:
                return; // Exit to previous menu
        }

        Console.WriteLine("Press any key to return to the menu...");
        Console.ReadKey();
    }
}
