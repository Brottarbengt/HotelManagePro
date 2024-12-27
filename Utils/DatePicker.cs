using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagePro.Utils;

public static class DatePicker
{
    public static DateOnly PickDate()
    {
        DateTime currentDate = DateTime.Now;
        DateTime selectedDate = new DateTime(currentDate.Year, currentDate.Month, 1);

        while (true)
        {
            Console.Clear();
            RenderCalendar(selectedDate);

            // Läsa användarens tangent
            var key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.RightArrow:
                    selectedDate = selectedDate.AddDays(1);
                    break;
                case ConsoleKey.LeftArrow:
                    selectedDate = selectedDate.AddDays(-1);
                    break;
                case ConsoleKey.UpArrow:
                    selectedDate = selectedDate.AddDays(-7);
                    break;
                case ConsoleKey.DownArrow:
                    selectedDate = selectedDate.AddDays(7);
                    break;
                case ConsoleKey.Enter:
                    return DateOnly.FromDateTime(selectedDate);
                
        }
    }
    static void RenderCalendar(DateTime selectedDate)
    {
        var calendarContent = new StringWriter();

        // Kalenderhuvud
        calendarContent.WriteLine($"[red]{selectedDate:MMMM}[/]".ToUpper());
        calendarContent.WriteLine("Mån  Tis  Ons  Tor  Fre  Lör  Sön");
        calendarContent.WriteLine("─────────────────────────────────");

        DateTime firstDayOfMonth = new DateTime(selectedDate.Year, selectedDate.Month, 1);
        int daysInMonth = DateTime.DaysInMonth(selectedDate.Year, selectedDate.Month);
        int startDay = (int)firstDayOfMonth.DayOfWeek;
        startDay = (startDay == 0) ? 6 : startDay - 1; 

        for (int i = 0; i < startDay; i++)
        {
            calendarContent.Write("     ");
        }

        for (int day = 1; day <= daysInMonth; day++)
        {
            if (day == selectedDate.Day)
            {
                calendarContent.Write($"[green]{day,2}[/]   ");
            }
            else
            {
                calendarContent.Write($"{day,2}   ");
            }

            if ((startDay + day) % 7 == 0)
            {
                calendarContent.WriteLine();
            }
        }

        var panel = new Panel(calendarContent.ToString())
        {
            Border = BoxBorder.Double,
            Header = new PanelHeader(($"[red]{selectedDate:yyyy}[/]"), Justify.Center)
        };

        AnsiConsole.Write(panel);
        Console.WriteLine();
        AnsiConsole.MarkupLine("\nUse Arrow Keys [blue]\u25C4 \u25B2 \u25BA \u25BC[/] to \nnavigate and [green]Enter[/] to confirm.");
    }
}
