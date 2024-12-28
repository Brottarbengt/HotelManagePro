using HotelManagePro.Database;
using HotelManagePro.Features.Bookings.Models;
using HotelManagePro.Features.Customers.Models;
using HotelManagePro.Features.Invoices.Models;
using HotelManagePro.Features.Rooms.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagePro.Utils
{
    public static class DataInitializer
    {
        public static void InitializeAndSeed(ApplicationDbContext dbContext)
        {

            dbContext.Database.Migrate();

            //   SEEDING

            // ROOMS
            if (!dbContext.Rooms.Any())
            {
                var rooms = new List<Room>();

                for (int floor = 0; floor < 3; floor++)
                {
                    for (int room = 1; room <= 10; room++)
                    {
                        var roomNumber = int.Parse($"{floor}{room:D2}");
                        rooms.Add(new Room
                        {
                            RoomNumber = roomNumber,
                            RoomType = room <= 6 ? TypeOfRoom.Single : TypeOfRoom.Double,
                            Size = room <= 6 ? 12 : 22, // Example sizes
                            IsActive = true,
                            ExtraBeds = room <= 6 ? 0 : (room <= 9 ? 1 : 2) // Logic for extra beds
                        });
                    }
                }

                dbContext.Rooms.AddRange(rooms);
            }

            // CUSTOMERS
            if (!dbContext.Customers.Any())
            {
                dbContext.Customers.AddRange(new List<Customer>
                {
                    new Customer
                    {
                        FirstName = "Karl",
                        LastName = "Westergren",
                        Email = "Karlw@tjohoo.com",
                        PhoneNumber = 0730263294,
                        Address = "821 41 Bollnäs",
                        IsActive = true,
                        Bookings = new List<Booking>()
                    },
                    new Customer
                    {
                        FirstName = "Arnold",
                        LastName = "Swartznegger",
                        Email = "t1000@skynet.com",
                        PhoneNumber = 027816176,
                        Address = "456 Elm St, Townsville",
                        IsActive = true,
                        Bookings = new List<Booking>()
                    }
                });
            }
            dbContext.SaveChanges();

            // INVOICES
            if (!dbContext.Invoices.Any())
            {
                dbContext.Invoices.Add(new Invoice
                {
                    TotalSum = 0,
                    IsPaid = false
                });
            }
                        
            dbContext.SaveChanges();
        }
    }
}
