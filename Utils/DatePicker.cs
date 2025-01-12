using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagePro.Utils;

public static class DatePicker
{
    public static DateOnly PickDate(string headline)
    {
        var today = DateOnly.FromDateTime(DateTime.Now);
        var maxDate = today.AddMonths(6);
        var currentDate = today;

        while (true)
        {
            Console.Clear();
            AnsiConsole.Write(new Rule($"[blue]{headline}[/]").RuleStyle("grey").Centered());
            DisplayCalendar(currentDate, today, maxDate);

            var key = Console.ReadKey(true).Key;
            var newDate = currentDate;

            switch (key)
            {
                case ConsoleKey.LeftArrow:
                    newDate = currentDate.AddDays(-1);
                    break;
                case ConsoleKey.RightArrow:
                    newDate = currentDate.AddDays(1);
                    break;
                case ConsoleKey.UpArrow:
                    newDate = currentDate.AddDays(-7);
                    break;
                case ConsoleKey.DownArrow:
                    newDate = currentDate.AddDays(7);
                    break;
                case ConsoleKey.Enter:
                    return currentDate;
            }

            if (newDate >= today && newDate <= maxDate)
                currentDate = newDate;
        }
    }

    private static void DisplayCalendar(DateOnly currentDate, DateOnly minDate, DateOnly maxDate)
    {
        var table = new Table()
            .Border(TableBorder.Simple)
            .BorderColor(Color.Grey)
            .Title($"[yellow]{currentDate:MMMM yyyy}[/]")
            .AddColumns("Mo", "Tu", "We", "Th", "Fr", "Sa", "Su");

        var firstDayOfMonth = new DateOnly(currentDate.Year, currentDate.Month, 1);
        var daysInMonth = DateTime.DaysInMonth(currentDate.Year, currentDate.Month);
        var dayOfWeek = ((int)firstDayOfMonth.DayOfWeek + 6) % 7;

        var weekDays = new List<string>();
        
        // Add leading spaces
        for (int i = 0; i < dayOfWeek; i++)
            weekDays.Add(" ");

        // Add days
        for (int day = 1; day <= daysInMonth; day++)
        {
            var date = new DateOnly(currentDate.Year, currentDate.Month, day);
            string dayText;

            if (date < minDate || date > maxDate)
                dayText = "[grey]" + day.ToString().PadLeft(2) + "[/]";
            else if (date == currentDate)
                dayText = $"[green]{day,2}[/]";
            else
                dayText = $"{day,2}";

            weekDays.Add(dayText);

            if (weekDays.Count == 7)
            {
                table.AddRow(weekDays.ToArray());
                weekDays.Clear();
            }
        }

        // Add remaining spaces to complete the last week
        while (weekDays.Count > 0 && weekDays.Count < 7)
            weekDays.Add(" ");

        if (weekDays.Count == 7)
            table.AddRow(weekDays.ToArray());

        AnsiConsole.Write(table);
        AnsiConsole.MarkupLine("\nUse [green]Arrow keys[/] to choose date");       
        AnsiConsole.MarkupLine("Press [green]Enter[/] to select date");
    }
}

