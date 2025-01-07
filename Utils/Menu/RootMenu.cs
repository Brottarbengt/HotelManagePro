using Spectre.Console;

namespace HotelManagePro.Utils.Menu;

public abstract class RootMenu
{
    protected readonly MenuNavigator _menuNavigator;
    protected readonly List<IMenuItem> _menuItems = new();
    protected abstract string MenuTitle { get; }

    protected RootMenu(MenuNavigator menuNavigator)
    {
        _menuNavigator = menuNavigator;
        InitializeMenuItems();
    }

    protected abstract void InitializeMenuItems();

    public virtual void Show()
    {
        while (true)
        {
            Console.Clear();
            var menuOptions = _menuItems.Select(m => m.Name).ToArray();
            
            AnsiConsole.Write(new Rule($"[blue]{MenuTitle}[/]").RuleStyle("grey").Centered());
            AnsiConsole.WriteLine();

            var selection = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("What would you like to do?")
                    .PageSize(10)
                    .AddChoices(menuOptions));

            var selectedIndex = Array.IndexOf(menuOptions, selection);
            if (selectedIndex >= 0)
            {
                _menuItems[selectedIndex].Execute();
            }
        }
    }
}
