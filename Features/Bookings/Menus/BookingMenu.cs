using HotelManagePro.Features.Bookings.Controller;
using HotelManagePro.Features.Bookings.Services;
using HotelManagePro.Utils.Menu;

namespace HotelManagePro.Features.Bookings.Menus;

public class BookingMenu : RootMenu
{
    private readonly BookingController _bookingController;
    protected override string MenuTitle => "Booking Menu";

    public BookingMenu(MenuNavigator menuNavigator, BookingController bookingController) 
        : base(menuNavigator)
    {
        _bookingController = bookingController;
    }

    protected override void InitializeMenuItems()
    {
        _menuItems.AddRange(new List<IMenuItem>
        {
            new MenuItem("Create New Booking", () => _bookingController.CreateNewBooking()),
            new MenuItem("Show All Bookings", () => _bookingController.ShowAllBookings()),
            new MenuItem("Search Booking by Email", () => _bookingController.SearchActiveBookingByEmail()),
            new MenuItem("Update Booking", () => _bookingController.UpdateBooking()),
            new MenuItem("Delete Booking", () => _bookingController.RemoveBooking()),
            new MenuItem("Back to Main Menu", () => _menuNavigator.NavigateToTopMenu())
        });
    }
}


