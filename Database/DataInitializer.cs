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
                        var roomType = room <= 6 ? TypeOfRoom.Single : TypeOfRoom.Double;
                        rooms.Add(new Room
                        {
                            RoomNumber = roomNumber,
                            RoomType = roomType,
                            Size = roomType == TypeOfRoom.Single ? 12 : 22,
                            IsActive = true,
                            Price = roomType == TypeOfRoom.Single ? 650 : 1200
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
                        DateOfBirth = new DateOnly(1984, 11, 09),
                        IsActive = true,
                        Address = new Address
                        {
                            StreetName = "Moravägen",
                            HouseNumber = "6",
                            PostalCode = "821 41",
                            City = "Bollnäs"
                        }
                    },
                    new Customer
                    {
                        FirstName = "Arnold",
                        LastName = "Swartznegger",
                        Email = "t1000@skynet.com",
                        PhoneNumber = 027816176,
                        DateOfBirth = new DateOnly(1990, 1, 1),
                        IsActive = true,
                        Address = new Address
                        {
                            StreetName = "Skystreet",
                            HouseNumber = "1",
                            PostalCode = "101 sky",
                            City = "Washington"
                        }
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
