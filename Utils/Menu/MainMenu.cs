using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManagePro.Features.Bookings.Menus;
using HotelManagePro.Features.Customers.Menus;
using HotelManagePro.Features.Rooms.Menus;
using HotelManagePro.Features.Invoices.Menus;

namespace HotelManagePro.Utils.Menu;

public class MainMenu : RootMenu
{
    private readonly BookingMenu _bookingMenu;
    private readonly CustomerMenu _customerMenu;
    private readonly RoomMenu _roomMenu;
    private readonly InvoiceMenu _invoiceMenu;
    protected override string MenuTitle => "Main Menu";

    public MainMenu(
        MenuNavigator menuNavigator,
        BookingMenu bookingMenu,
        CustomerMenu customerMenu,
        RoomMenu roomMenu,
        InvoiceMenu invoiceMenu) 
        : base(menuNavigator)
    {
        _bookingMenu = bookingMenu;
        _customerMenu = customerMenu;
        _roomMenu = roomMenu;
        _invoiceMenu = invoiceMenu;
    }

    protected override void InitializeMenuItems()
    {
        _menuItems.AddRange(new List<IMenuItem>
        {
            new MenuItem("Bookings", () => _bookingMenu.Show()),
            new MenuItem("Customers", () => _customerMenu.Show()),
            new MenuItem("Rooms", () => _roomMenu.Show()),
            new MenuItem("Invoices", () => _invoiceMenu.Show()),
            new MenuItem("Exit", () => Environment.Exit(0))
        });
    }
}
