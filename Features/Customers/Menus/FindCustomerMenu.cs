using HotelManagePro.Features.Customers.Controller;
using HotelManagePro.Utils.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagePro.Features.Customers.Menus;

public class FindCustomerMenu : RootMenu
{
    private readonly CustomerController _customerController;
    protected override string MenuTitle => "Find Customer";

    public FindCustomerMenu(MenuNavigator menuNavigator, CustomerController customerController) 
        : base(menuNavigator)
    {
        _customerController = customerController;
    }

    protected override void InitializeMenuItems()
    {
        _menuItems.AddRange(new List<IMenuItem>
        {
            new MenuItem("Find by Customer ID", () => _customerController.FindCustomerById()),
            new MenuItem("Find by Email", () => _customerController.FindCustomerByEmail()),
            new MenuItem("Find by Phone", () => _customerController.FindCustomerByPhone()),
            new MenuItem("New Customer", () => _customerController.CreateNewCustomer()),
            new MenuItem("Back to Customer Menu", () => _menuNavigator.NavigateToTopMenu())
        });
    }
}
