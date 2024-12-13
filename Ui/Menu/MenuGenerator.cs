using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagePro.Display.Menu
{
    public static class MenuGenerator
    {
        public static void ShowMenu(string title, string[] options, Action<int> executeOption)
        {
            int selectedIndex = 0;

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"{title}:");

                for (int i = 0; i < options.Length; i++)
                {
                    if (i == selectedIndex)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"   {options[i]} ");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(options[i]);
                    }
                }

                Console.ResetColor();
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    selectedIndex--;
                    if (selectedIndex < 0) selectedIndex = options.Length - 1;
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    selectedIndex++;
                    if (selectedIndex >= options.Length) selectedIndex = 0;
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    executeOption(selectedIndex);
                }
            }
        }
    }
}
