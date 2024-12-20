using HotelManagePro.Database;
using HotelManagePro.Features.Booking.Controller;
using HotelManagePro.Features.Booking.Services;
using HotelManagePro.Features.Booking.Services.HotelManagePro.Features.Booking.Services;
using HotelManagePro.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace HotelManagePro
{
    public class App
    {
        public void Run()
        {
            
            var builder = new ConfigurationBuilder().AddJsonFile($"appsettings.json", true, true);
            var config = builder.Build();

           
            var options = new DbContextOptionsBuilder<ApplicationDbContext>();
            var connectionString = config.GetConnectionString("DefaultConnection");
            options.UseSqlServer(connectionString);

            
            var serviceProvider = new ServiceCollection()
                .AddSingleton(config) 
                .AddDbContext<ApplicationDbContext>(opts => opts.UseSqlServer(connectionString)) 
                .AddScoped<BookingController>() 
                .AddScoped<BookingService>() 
                .BuildServiceProvider();

            
            using (var scope = serviceProvider.CreateScope())
            {
                var bookingService = scope.ServiceProvider.GetRequiredService<BookingService>();
                var bookingController = scope.ServiceProvider.GetRequiredService<BookingController>();

                
                BookingMenu.SetBookingService(bookingService);

               
                using (var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>())
                {
                    DataInitializer.InitializeAndSeed(dbContext); 

               
                BookingMenu.ShowMenu(); 
            }


            /* 


             * TODO

             * Det blir En Validator per Feature
             * Bygga en CenterAll() för bättre UX?
             * Behöver bena ut hur Menu ska vara strukturerad
             * Kommer MnuGenerator fungera för alla menyer?
             * Bygga DTOs efter behov


            ===============  BOOKING  ====================
                - Vilka Props/Attributer behövs OCH vilka ska kunna vara NULL!?
                - Om man gör en ny booking OCH har ny kund, ska man kunna adda ny kund direkt? en prompt innan man 
                  startar ny booking som frågar 'Ny Kund?' Ja = -> newCustomer, Nej = -> ShowAllCustomer() följt av NewBoooking()



            ===============  ROOMS  ====================
                - Vilka Props/Attributer behövs OCH vilka ska kunna vara NULL!?
                - 


            ===============  CUSTOMER  ====================
                - Vilka Props/Attributer behövs OCH vilka ska kunna vara NULL!?


            ===============  INVOICE  ====================
                - Vilka Props/Attributer behövs OCH vilka ska kunna vara NULL!?




             This Hotel:

             A three-story hotel with ten rooms each level.
             6 Single and 4 Doubles each.
             1 - 6 is single and 7 - 10 is double
             RoomNumbers start with level followed by roomnumber.
             Ex. Room 010 is the tenth room on ground floor hence a double.
                 Room 106 is the sixth room on second floor hence a single.



            */
        }
}
