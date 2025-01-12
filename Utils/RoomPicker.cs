using HotelManagePro.Features.Rooms.Models;
using Spectre.Console;

namespace HotelManagePro.Utils;

public class RoomPicker
{
    public static List<Room> PickRooms(List<Room> rooms)
    {
        int selectedFloor = 0;
        int selectedRoomIndex = 0;
        var selectedRooms = new HashSet<int>();

        while (true)
        {
            Console.Clear();
            RenderRooms(rooms, selectedFloor, selectedRoomIndex, selectedRooms);

            if (selectedRooms.Count != 0)
            {
                var selectedRoomNumbers = rooms
                    .Where(r => selectedRooms.Contains(r.RoomNumber))
                    .Select(r => r.RoomNumber)
                    .OrderBy(n => n);
                AnsiConsole.MarkupLine($"\n[blue]Selected Room numbers:[/] {string.Join(", ", selectedRoomNumbers)}");
            }

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
                case ConsoleKey.Spacebar:
                    ToggleRoomSelection(rooms, selectedFloor, selectedRoomIndex, selectedRooms);
                    break;
                case ConsoleKey.Escape:
                    Console.WriteLine("\nBooking canceled.");
                    return [];
                case ConsoleKey.Enter:
                    if (selectedRooms.Count == 0)
                    {
                        AnsiConsole.MarkupLine("\n[red]No room selected, please select one or more rooms or press Escape to cancel booking[/]");
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey(true);
                        continue;
                    }
                    return rooms.Where(r => selectedRooms.Contains(r.RoomNumber)).ToList();
            }
        }
    }

    private static void RenderRooms(List<Room> rooms, int selectedFloor, int selectedRoomIndex, HashSet<int> selectedRooms)
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
                ? GetRoomMarkup(singleRooms[i], selectedRoomIndex == i, selectedRooms.Contains(singleRooms[i].RoomNumber))
                : "";
            string doubleRoomText = (i >= 6 && (i - 6) < doubleRooms.Count)
                ? GetRoomMarkup(doubleRooms[i - 6], selectedRoomIndex == i, selectedRooms.Contains(doubleRooms[i - 6].RoomNumber))
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
        AnsiConsole.MarkupLine("\nUse [green]Arrow Keys[/] to navigate, [green]Space[/] to select/deselect, and [green]Enter[/] to confirm.");
    }

    private static string GetRoomMarkup(Room room, bool isSelected, bool isRoomSelected)
    {
        var markup = isSelected ? "[green]" : "";
        markup += isRoomSelected ? $"[bold]Room {room.RoomNumber} ({room.RoomType})[/]" : $"Room {room.RoomNumber} ({room.RoomType})";
        markup += isSelected ? "[/]" : "";
        return markup;
    }

    private static void ToggleRoomSelection(List<Room> rooms, int selectedFloor, int selectedRoomIndex, HashSet<int> selectedRooms)
    {
        int roomNumber = selectedFloor * 100 + (selectedRoomIndex + 1);
        var room = rooms.FirstOrDefault(r => r.RoomNumber == roomNumber);

        if (room != null)
        {
            if (selectedRooms.Contains(room.RoomNumber))
                selectedRooms.Remove(room.RoomNumber);
            else
                selectedRooms.Add(room.RoomNumber);
        }
    }
}
