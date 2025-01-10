using HotelManagePro.Features.Bookings.Models;
using HotelManagePro.Features.Customers.Models;
using HotelManagePro.Features.Invoices.Models;
using HotelManagePro.Features.Rooms.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagePro.Database
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Booking> Booking { get; set; }
        public DbSet<Room> Room { get; set; } 
        public DbSet<Invoice> Invoice { get; set; }

        public ApplicationDbContext()
        {
        }

        /// <summary>
        /// Konstruktor med alternativ (options):
        /// Denna konstruktor tar in inställningar som skickas från appens konfiguration,
        /// t.ex. anslutningssträngen.
        /// </summary>
        /// <param name="options"></param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        /// <summary>
        /// Metoden `OnConfiguring`: används första gången applikationen körs för att
        /// koppla databasen till rätt server.
        /// Om anslutningssträngen inte redan är inställd, anger vi en direkt här.
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=localhost;Database=HMP;Trusted_Connection=True;TrustServerCertificate=true;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Invoice>()
                .HasKey(i => i.InvoiceId);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Invoice)
                .WithOne()
                .HasForeignKey<Invoice>(i => i.BookingId)
                .IsRequired();
        }

    }
}
