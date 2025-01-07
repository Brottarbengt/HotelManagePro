namespace HotelManagePro.Utils.Menu;

public class MenuItem : IMenuItem
{
    public string Name { get; }
    private readonly Action _action;

    public MenuItem(string name, Action action)
    {
        Name = name;
        _action = action;
    }

    public void Execute() => _action();
} 