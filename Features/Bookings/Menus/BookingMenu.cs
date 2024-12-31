using HotelManagePro.Features.Bookings.Controller;
using HotelManagePro.Features.Bookings.Services;
using HotelManagePro.Utils.Menu;

namespace HotelManagePro.Features.Bookings.Menus;

public static class BookingMenu
{
    private static string[] menuOptions = new string[] {
        "New Booking",
        "Search for Booking",  
        "Show All Bookings", 
        "Remove Booking",
        "Edit Booking",
        "Back to Top Menu"
    };

    private static BookingController _bookingController;
    public static void SetBookingService(BookingController bookingController)
    {
        _bookingController = bookingController;
    }

    public static void ShowMenu()
    {
        MenuGenerator.ShowMenu("Bookings Menu", menuOptions, ExecuteSelectedOption);
    }

    private static void ExecuteSelectedOption(int selectedIndex)
    {
        switch (selectedIndex)
        {
            case 0:
                _bookingController.CreateNewBooking();
                break;
            case 1:
                _bookingController.SearchBookingByEmail();
                break;
            case 2:
                _bookingController.ShowAllBookings();
                break;
            case 3:
                _bookingController.RemoveBooking();
                break;
            case 4:
                _bookingController.EditBooking();
                break;
            case 5:
                TopMenu.ShowMenu();
                break;
            default:
                break;
        }
    }
}


