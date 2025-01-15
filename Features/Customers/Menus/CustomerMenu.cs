using HotelManagePro.Features.Customers.Controller;
using HotelManagePro.Utils.Menu;

namespace HotelManagePro.Features.Customers.Menus;

public class CustomerMenu : RootMenu
{
    private readonly CustomerController _customerController;
    
    protected override string MenuTitle => "Customer Menu";

    public CustomerMenu(MenuNavigator menuNavigator, CustomerController customerController) 
        : base(menuNavigator)
        
    {
        _customerController = customerController;
    }

    protected override void InitializeMenuItems()
    {
        _menuItems.AddRange(
        [
            new MenuItem("Create New Customer", () => _customerController.CreateNewCustomer()),            
            new MenuItem("Find and Update Customer", () => _customerController.FindCustomerForUpdate()),
            new MenuItem("Show All Customers", () => _customerController.ShowAllCustomers()),
            new MenuItem("Delete Customer( SOFT DELETE )", () => _customerController.SoftDeleteCustomer()),
            new MenuItem("Delete Customer( HARD DELETE )", () => _customerController.HardDeleteCustomer()),
            new MenuItem("Back to Main Menu", () => _menuNavigator.NavigateToTopMenu())
        ]);
    }
}
