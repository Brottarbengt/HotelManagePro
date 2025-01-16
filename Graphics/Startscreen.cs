using Spectre.Console;

namespace HotelManagePro.Graphics;

public static class Startscreen
{
    public static void Show()
    {
        Console.Clear();
        var table = new Table()
            .Border(TableBorder.None)
            .AddColumn(new TableColumn("").Centered())
            .HideHeaders()
            .Centered();

        var logo = new Panel(@"[blue]
    ██╗  ██╗███╗   ███╗██████╗ 
    ██║  ██║████╗ ████║██╔══██╗
    ███████║██╔████╔██║██████╔╝
    ██╔══██║██║╚██╔╝██║██╔═══╝ 
    ██║  ██║██║ ╚═╝ ██║██║     
    ╚═╝  ╚═╝╚═╝     ╚═╝╚═╝     [/]")
        {
            Border = BoxBorder.None
        };

        var title = new Panel("[blue]Hotel Manage Pro © 2024[/]")
        {
            Border = BoxBorder.None
        };

        table.AddRow(logo);
        table.AddRow(title);
        table.AddRow("[grey]Press any key to continue...[/]");

        AnsiConsole.Write(table);
        Console.ReadKey(true);
        Console.Clear();
    }
} 