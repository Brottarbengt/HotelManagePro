using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelManagePro.Migrations
{
    /// <inheritdoc />
    public partial class TestingPKnamechange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Customers_CustomerId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Invoices_InvoiceId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Bookings_BookingId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_CustomerId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "BookingId",
                table: "Rooms",
                newName: "BookingsId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Rooms",
                newName: "RoomsId");

            migrationBuilder.RenameIndex(
                name: "IX_Rooms_BookingId",
                table: "Rooms",
                newName: "IX_Rooms_BookingsId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Invoices",
                newName: "InvoicesId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Customers",
                newName: "CustomersId");

            migrationBuilder.RenameColumn(
                name: "InvoiceId",
                table: "Bookings",
                newName: "InvoicesId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Bookings",
                newName: "BookingsId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_InvoiceId",
                table: "Bookings",
                newName: "IX_Bookings_InvoicesId");

            migrationBuilder.AddColumn<int>(
                name: "CustomersId",
                table: "Bookings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_CustomersId",
                table: "Bookings",
                column: "CustomersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Customers_CustomersId",
                table: "Bookings",
                column: "CustomersId",
                principalTable: "Customers",
                principalColumn: "CustomersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Invoices_InvoicesId",
                table: "Bookings",
                column: "InvoicesId",
                principalTable: "Invoices",
                principalColumn: "InvoicesId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Bookings_BookingsId",
                table: "Rooms",
                column: "BookingsId",
                principalTable: "Bookings",
                principalColumn: "BookingsId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Customers_CustomersId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Invoices_InvoicesId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Bookings_BookingsId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_CustomersId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "CustomersId",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "BookingsId",
                table: "Rooms",
                newName: "BookingId");

            migrationBuilder.RenameColumn(
                name: "RoomsId",
                table: "Rooms",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Rooms_BookingsId",
                table: "Rooms",
                newName: "IX_Rooms_BookingId");

            migrationBuilder.RenameColumn(
                name: "InvoicesId",
                table: "Invoices",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CustomersId",
                table: "Customers",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "InvoicesId",
                table: "Bookings",
                newName: "InvoiceId");

            migrationBuilder.RenameColumn(
                name: "BookingsId",
                table: "Bookings",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_InvoicesId",
                table: "Bookings",
                newName: "IX_Bookings_InvoiceId");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_CustomerId",
                table: "Bookings",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Customers_CustomerId",
                table: "Bookings",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Invoices_InvoiceId",
                table: "Bookings",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Bookings_BookingId",
                table: "Rooms",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
