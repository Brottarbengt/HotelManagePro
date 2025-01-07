namespace HotelManagePro.Utils.Menu;

public interface IMenuItem
{
    string Name { get; }
    void Execute();
} 