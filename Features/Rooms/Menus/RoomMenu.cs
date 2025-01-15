using HotelManagePro.Features.Rooms.Controller;
using HotelManagePro.Utils.Menu;

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
        _menuItems.AddRange(
        [
            new MenuItem("Add New Room", () => _roomController.CreateNewRoom()),
            new MenuItem("Show Status on all Rooms", () => _roomController.ShowStatusAllRooms()),
            new MenuItem("Show Available Rooms by Dates", () => _roomController.ShowAvailableRooms()),
            new MenuItem("Edit Room Details", () => _roomController.EditRoomDetails()),
            new MenuItem("Delete Room", () => _roomController.DeleteRoom()),
            new MenuItem("Back to Main Menu", () => _menuNavigator.NavigateToTopMenu())
        ]);
    }
}
