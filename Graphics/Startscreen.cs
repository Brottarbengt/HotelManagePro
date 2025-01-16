namespace HotelManagePro.Graphics;

public static class Startscreen
{
    public static void Show()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(@"
    ██╗  ██╗███╗   ███╗██████╗ 
    ██║  ██║████╗ ████║██╔══██╗
    ███████║██╔████╔██║██████╔╝
    ██╔══██║██║╚██╔╝██║██╔═══╝ 
    ██║  ██║██║ ╚═╝ ██║██║     
    ╚═╝  ╚═╝╚═╝     ╚═╝╚═╝     
                                
    Hotel Manage Pro © 2024
    ");
        Console.ResetColor();
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey(true);
        Console.Clear();
    }
} 