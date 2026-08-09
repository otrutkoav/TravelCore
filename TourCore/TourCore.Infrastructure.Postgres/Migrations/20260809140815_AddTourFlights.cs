using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TourCore.Infrastructure.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class AddTourFlights : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TourFlightGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TourServiceId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Priority = table.Column<int>(type: "integer", nullable: false),
                    OrderIndex = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourFlightGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourFlightGroups_TourServices_TourServiceId",
                        column: x => x.TourServiceId,
                        principalTable: "TourServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TourFlights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TourFlightGroupId = table.Column<int>(type: "integer", nullable: false),
                    OrderIndex = table.Column<int>(type: "integer", nullable: false),
                    DepartureCountryIds = table.Column<string>(type: "text", nullable: true),
                    DepartureCityIds = table.Column<string>(type: "text", nullable: true),
                    ArrivalCountryIds = table.Column<string>(type: "text", nullable: true),
                    ArrivalCityIds = table.Column<string>(type: "text", nullable: true),
                    AirlineIds = table.Column<string>(type: "text", nullable: true),
                    FlightIds = table.Column<string>(type: "text", nullable: true),
                    DepartureAirportIds = table.Column<string>(type: "text", nullable: true),
                    ArrivalAirportIds = table.Column<string>(type: "text", nullable: true),
                    FareIds = table.Column<string>(type: "text", nullable: true),
                    MinConnectionMinutes = table.Column<int>(type: "integer", nullable: true),
                    MaxConnectionMinutes = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourFlights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourFlights_TourFlightGroups_TourFlightGroupId",
                        column: x => x.TourFlightGroupId,
                        principalTable: "TourFlightGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TourFlightGroups_TourServiceId_OrderIndex",
                table: "TourFlightGroups",
                columns: new[] { "TourServiceId", "OrderIndex" });

            migrationBuilder.CreateIndex(
                name: "IX_TourFlights_TourFlightGroupId_OrderIndex",
                table: "TourFlights",
                columns: new[] { "TourFlightGroupId", "OrderIndex" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TourFlights");

            migrationBuilder.DropTable(
                name: "TourFlightGroups");
        }
    }
}
