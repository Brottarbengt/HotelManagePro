namespace HotelManagePro.Utils.Menu;

public class MenuNavigator
{
    private MainMenu? _mainMenu;

    public void SetTopMenu(MainMenu mainMenu)
    {
        _mainMenu = mainMenu;
    }

    public void NavigateToTopMenu()
    {
        _mainMenu?.Show();
    }
} 