using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking_system_API.Migrations
{
    /// <inheritdoc />
    public partial class flightlast : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Flights_FlightGuid",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "FlightGuid",
                table: "Bookings",
                newName: "Flightid");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_FlightGuid",
                table: "Bookings",
                newName: "IX_Bookings_Flightid");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Flights_Flightid",
                table: "Bookings",
                column: "Flightid",
                principalTable: "Flights",
                principalColumn: "FlightId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Flights_Flightid",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "Flightid",
                table: "Bookings",
                newName: "FlightGuid");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_Flightid",
                table: "Bookings",
                newName: "IX_Bookings_FlightGuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Flights_FlightGuid",
                table: "Bookings",
                column: "FlightGuid",
                principalTable: "Flights",
                principalColumn: "FlightId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
