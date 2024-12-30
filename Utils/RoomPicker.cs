using HotelManagePro.Features.Rooms.Models;
using Spectre.Console;

namespace HotelManagePro.Utils;

public class RoomPicker
{

    public static Room PickRoom(List<Room> rooms)
    {
        int selectedFloor = 0;
        int selectedRoomIndex = 0;

        //var rooms = rooms;  <- Bhövs denna av någon anledning

        while (true)
        {
            Console.Clear();
            RenderRooms(rooms, selectedFloor, selectedRoomIndex);

            var key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.RightArrow:
                    selectedRoomIndex = (selectedRoomIndex + 1) % 10;
                    break;
                case ConsoleKey.LeftArrow:
                    selectedRoomIndex = (selectedRoomIndex - 1 + 10) % 10;
                    break;
                case ConsoleKey.UpArrow:
                    selectedFloor = (selectedFloor - 1 + 3) % 3;
                    break;
                case ConsoleKey.DownArrow:
                    selectedFloor = (selectedFloor + 1) % 3;
                    break;
                case ConsoleKey.Enter:
                    int roomNumber = selectedFloor * 100 + (selectedRoomIndex + 1);
                    return rooms.First(r => r.RoomNumber == roomNumber);
            }
        }
    }

    private static void RenderRooms(List<Room> rooms, int selectedFloor, int selectedRoomIndex)
    {
        var table = new Table()
            .Centered()
            .AddColumn(new TableColumn("[red]Single Rooms[/]").Centered())
            .AddColumn(new TableColumn("[blue]Double Rooms[/]").Centered());

        var singleRooms = rooms.Where(r => r.RoomType == TypeOfRoom.Single && r.RoomNumber / 100 == selectedFloor).ToList();
        var doubleRooms = rooms.Where(r => r.RoomType == TypeOfRoom.Double && r.RoomNumber / 100 == selectedFloor).ToList();

        for (int i = 0; i < 10; i++)
        {
            string singleRoomText = (i < 6 && i < singleRooms.Count)
                ? GetRoomMarkup(singleRooms[i], selectedRoomIndex == i)
                : "";
            string doubleRoomText = (i >= 6 && (i - 6) < doubleRooms.Count)
                ? GetRoomMarkup(doubleRooms[i - 6], selectedRoomIndex == i)
                : "";

            table.AddRow(singleRoomText, doubleRoomText);
        }

        var panel = new Panel(table)
        {
            Border = BoxBorder.Double,
            Header = new PanelHeader($"[yellow]Floor {selectedFloor}[/]", Justify.Center)
        };

        AnsiConsole.Write(panel);
        Console.WriteLine();
        AnsiConsole.MarkupLine("\nUse Arrow Keys [blue]\u25C4 \u25B2 \u25BA \u25BC[/] to \nnavigate and [green]Enter[/] to confirm.");
    }

    private static string GetRoomMarkup(Room room, bool isSelected)
    {
        return isSelected
            ? $"[green]Room {room.RoomNumber} ({room.RoomType})[/]"
            : $"Room {room.RoomNumber} ({room.RoomType})";
    }


}
