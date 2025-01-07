using HotelManagePro.Features.Customers.Controller;
using HotelManagePro.Utils.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagePro.Features.Customers.Menus;

public class CustomerMenu : RootMenu
{
    private readonly CustomerController _customerController;
    private readonly FindCustomerMenu _findCustomerMenu;
    protected override string MenuTitle => "Customer Menu";

    public CustomerMenu(
        MenuNavigator menuNavigator,
        CustomerController customerController,
        FindCustomerMenu findCustomerMenu) 
        : base(menuNavigator)
    {
        _customerController = customerController;
        _findCustomerMenu = findCustomerMenu;
    }

    protected override void InitializeMenuItems()
    {
        _menuItems.AddRange(new List<IMenuItem>
        {
            new MenuItem("Find Customer", () => _findCustomerMenu.Show()),
            new MenuItem("Create New Customer", () => _customerController.CreateNewCustomer()),
            new MenuItem("Show All Customers", () => _customerController.ShowAllCustomers()),
            new MenuItem("Back to Main Menu", () => _menuNavigator.NavigateToTopMenu())
        });
    }
}
