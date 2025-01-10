using Bogus;
using HotelManagePro.Database;
using HotelManagePro.Features.Bookings.Models;
using HotelManagePro.Features.Customers.Models;
using HotelManagePro.Features.Invoices.Models;
using HotelManagePro.Features.Rooms.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagePro.Database;

public static class DataInitializer
{
    public static void InitializeAndSeed(ApplicationDbContext dbContext)
    {
        dbContext.Database.Migrate();

        if (!dbContext.Room.Any())
        {
            SeedRooms(dbContext);
        }

        if (!dbContext.Customer.Any())
        {
            SeedCustomers(dbContext);
        }
    }

    private static void SeedRooms(ApplicationDbContext dbContext)
    {
        var rooms = new List<Room>();

        // 3 våningar (0-2), 10 rum per våning
        for (int floor = 0; floor <= 2; floor++)
        {
            for (int roomNum = 1; roomNum <= 10; roomNum++)
            {
                var roomNumber = (floor * 100) + roomNum;
                var roomType = roomNum <= 6 ? TypeOfRoom.Single : TypeOfRoom.Double;
                
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

        dbContext.Room.AddRange(rooms);
        dbContext.SaveChanges();
    }

    private static void SeedCustomers(ApplicationDbContext dbContext)
    {
        var faker = new Faker("sv");
        
        var customers = new Faker<Customer>("sv")
            .RuleFor(c => c.FirstName, f => f.Name.FirstName())
            .RuleFor(c => c.LastName, f => f.Name.LastName())
            .RuleFor(c => c.Email, (f, c) => f.Internet.Email(c.FirstName, c.LastName))
            .RuleFor(c => c.PhoneNumber, f => int.Parse("0" + f.Random.Number(700000000, 799999999).ToString()))
            .RuleFor(c => c.DateOfBirth, f => DateOnly.FromDateTime(f.Date.Past(50, DateTime.Now.AddYears(-18))))
            .RuleFor(c => c.IsActive, f => true)
            .RuleFor(c => c.Address, f => new Address
            {
                StreetName = f.Address.StreetName(),
                HouseNumber = f.Random.Number(1, 100).ToString(),
                PostalCode = f.Address.ZipCode(),
                City = f.Address.City()
            })
            .Generate(20);

        dbContext.Customer.AddRange(customers);
        dbContext.SaveChanges();
    }
}
