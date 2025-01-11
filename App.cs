using HotelManagePro.Database;
using HotelManagePro.Features.Bookings.Controller;
using HotelManagePro.Features.Bookings.Menus;
using HotelManagePro.Features.Bookings.Services;
using HotelManagePro.Features.Customers.Controller;
using HotelManagePro.Features.Customers.Services;
using HotelManagePro.Features.Invoices.Services;
using HotelManagePro.Features.Invoices.Controller;
using HotelManagePro.Features.Rooms.Controller;
using HotelManagePro.Features.Rooms.Services;
using HotelManagePro.Utils;
using HotelManagePro.Utils.Menu;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using HotelManagePro.Features.Customers.Menus;
using HotelManagePro.Features.Rooms.Menus;
using HotelManagePro.Features.Invoices.Menus;


namespace HotelManagePro;

public class App
{
    public void Run()
    {
        
        var builder = new ConfigurationBuilder().AddJsonFile($"appsettings.json", true, true);
        var config = builder.Build();

       
        var options = new DbContextOptionsBuilder<ApplicationDbContext>();
        var connectionString = config.GetConnectionString("DefaultConnection");
        options.UseSqlServer(connectionString);

        using (var dbContext = new ApplicationDbContext(options.Options))
	    {
	        dbContext.Database.Migrate();
	        DataInitializer.InitializeAndSeed(dbContext);
	    }

        
        var serviceProvider = new ServiceCollection()
            .AddSingleton(config) 
            .AddDbContext<ApplicationDbContext>(opts => opts.UseSqlServer(connectionString)) 
            .AddScoped<BookingController>() 
            .AddScoped<BookingService>()
            .AddScoped<CustomerService>()
            .AddScoped<CustomerController>()
            .AddScoped<InvoiceController>()
            .AddScoped<InvoiceService>()
            .AddScoped<RoomController>()
            .AddScoped<RoomService>()
            .AddSingleton<MenuNavigator>()
            .AddScoped<MainMenu>()
            .AddScoped<BookingMenu>()
            .AddScoped<CustomerMenu>()
            .AddScoped<RoomMenu>()
            .AddScoped<InvoiceMenu>()
            .AddScoped<FindCustomerMenu>()
            .BuildServiceProvider();


        using (var scope = serviceProvider.CreateScope())
        {
            var mainMenu = scope.ServiceProvider.GetRequiredService<MainMenu>();
            var menuNavigator = scope.ServiceProvider.GetRequiredService<MenuNavigator>();
            menuNavigator.SetTopMenu(mainMenu);

            mainMenu.Show();
        }
    }
}
        /* 
         
         This Hotel:

         A three-story hotel with ten rooms each level.
         6 Single and 4 Doubles each.
         1 - 6 is single and 7 - 10 is double
         RoomNumbers start with level followed by roomnumber.
         Ex. Rooms 010 is the tenth room on ground floor hence a double.
             Rooms 106 is the sixth room on second floor hence a single.

        */