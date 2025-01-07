using HotelManagePro.Features.Invoices.Controller;
using HotelManagePro.Utils.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagePro.Features.Invoices.Menus;

public class InvoiceMenu : RootMenu
{
    private readonly InvoiceController _invoiceController;
    protected override string MenuTitle => "Invoice Menu";

    public InvoiceMenu(MenuNavigator menuNavigator, InvoiceController invoiceController) 
        : base(menuNavigator)
    {
        _invoiceController = invoiceController;
    }

    protected override void InitializeMenuItems()
    {
        _menuItems.AddRange(new List<IMenuItem>
        {
            new MenuItem("Show All Invoices", () => _invoiceController.ShowAllInvoices()),
            new MenuItem("Search Invoice", () => _invoiceController.SearchInvoice()),
            new MenuItem("Mark Invoice as Paid", () => _invoiceController.MarkInvoiceAsPaid()),
            new MenuItem("Back to Main Menu", () => _menuNavigator.NavigateToTopMenu())
        });
    }
}
