using HotelManagePro.Features.Bookings.Controller;
using HotelManagePro.Features.Bookings.Services;
using HotelManagePro.Utils.Menu;

namespace HotelManagePro.Features.Bookings.Menus;

public static class ManageBookingMenu
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

    private static BookingController _bookingConroller;
    public static void SetBookingService(BookingController BookingController)
    {
        _bookingConroller = BookingController;
    }



    private static void ExecuteOption(int selectedIndex)
    {
        switch (selectedIndex)
        {
            case 0:
                _bookingConroller.EditBooking();
                break;
            case 1:
                _bookingConroller.RemoveBooking();
                break;
            case 2:
                return; // Exit to previous menu
        }

        Console.WriteLine("Press any key to return to the menu...");
        Console.ReadKey();
    }
}
// Why have this option? and What to show? maybe just BookingId, Name, Email and Dates?
// Maybe have navigation trou list, like datepicker but just up and down, and choose booking with enter.