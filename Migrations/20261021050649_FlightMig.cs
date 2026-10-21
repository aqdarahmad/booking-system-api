
using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking_system_API.Migrations
{
    public partial class FlightMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                ALTER TABLE "Tickets"
                ALTER COLUMN "TicketStatus" TYPE integer
                USING CASE "TicketStatus"
                    WHEN 'Pending' THEN 0
                    WHEN 'Confirmed' THEN 1
                    WHEN 'Cancelled' THEN 2
                    ELSE NULL
                END;
                """);

            migrationBuilder.AddColumn<Guid>(
                name: "FlightId",
                table: "Tickets",
                type: "uuid",
                nullable: false,
                defaultValue: Guid.Empty);

            migrationBuilder.AddColumn<Guid>(
                name: "FlightGuid",
                table: "Bookings",
                type: "uuid",
                nullable: false,
                defaultValue: Guid.Empty);

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    FlightId = table.Column<Guid>(
                        type: "uuid",
                        nullable: false),

                    DepartureDate = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: false),

                    ArrivalDate = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: false),

                    DepartureAirport = table.Column<string>(
                        type: "text",
                        nullable: false),

                    ArrivalAirport = table.Column<string>(
                        type: "text",
                        nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey(
                        "PK_Flights",
                        x => x.FlightId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_FlightId",
                table: "Tickets",
                column: "FlightId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_FlightGuid",
                table: "Bookings",
                column: "FlightGuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Flights_FlightGuid",
                table: "Bookings",
                column: "FlightGuid",
                principalTable: "Flights",
                principalColumn: "FlightId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Flights_FlightId",
                table: "Tickets",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "FlightId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Flights_FlightGuid",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Flights_FlightId",
                table: "Tickets");

            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_FlightId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_FlightGuid",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "FlightId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "FlightGuid",
                table: "Bookings");

            migrationBuilder.Sql("""
                ALTER TABLE "Tickets"
                ALTER COLUMN "TicketStatus" TYPE text
                USING CASE "TicketStatus"
                    WHEN 0 THEN 'Pending'
                    WHEN 1 THEN 'Confirmed'
                    WHEN 2 THEN 'Cancelled'
                    ELSE 'Pending'
                END;
                """);
        }
    }
}
