using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelManagePro.Migrations
{
    /// <inheritdoc />
    public partial class addedproponBookingmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookingId1",
                table: "Invoices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateOnly>(
                name: "CreatedAt",
                table: "Bookings",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_BookingId1",
                table: "Invoices",
                column: "BookingId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Bookings_BookingId1",
                table: "Invoices",
                column: "BookingId1",
                principalTable: "Bookings",
                principalColumn: "BookingId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Bookings_BookingId1",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_BookingId1",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "BookingId1",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Bookings");
        }
    }
}
