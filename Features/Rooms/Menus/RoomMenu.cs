using HotelManagePro.Features.Rooms.Controller;
using HotelManagePro.Utils.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagePro.Features.Rooms.Menus;

public class RoomMenu : RootMenu
{
    private readonly RoomController _roomController;
    protected override string MenuTitle => "Room Menu";

    public RoomMenu(MenuNavigator menuNavigator, RoomController roomController) 
        : base(menuNavigator)
    {
        _roomController = roomController;
    }

    protected override void InitializeMenuItems()
    {
        _menuItems.AddRange(new List<IMenuItem>
        {
            new MenuItem("Show All Rooms", () => _roomController.ShowAllRooms()),
            new MenuItem("Show Available Rooms", () => _roomController.ShowAvailableRooms()),
            new MenuItem("Show Room Details", () => _roomController.ShowRoomDetails()),
            new MenuItem("Back to Main Menu", () => _menuNavigator.NavigateToTopMenu())
        });
    }
}
