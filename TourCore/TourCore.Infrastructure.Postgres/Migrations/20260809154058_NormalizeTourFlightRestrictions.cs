using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TourCore.Infrastructure.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class NormalizeTourFlightRestrictions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AirlineIds",
                table: "TourFlights");

            migrationBuilder.DropColumn(
                name: "ArrivalAirportIds",
                table: "TourFlights");

            migrationBuilder.DropColumn(
                name: "ArrivalCityIds",
                table: "TourFlights");

            migrationBuilder.DropColumn(
                name: "ArrivalCountryIds",
                table: "TourFlights");

            migrationBuilder.DropColumn(
                name: "DepartureAirportIds",
                table: "TourFlights");

            migrationBuilder.DropColumn(
                name: "DepartureCityIds",
                table: "TourFlights");

            migrationBuilder.DropColumn(
                name: "DepartureCountryIds",
                table: "TourFlights");

            migrationBuilder.DropColumn(
                name: "FareIds",
                table: "TourFlights");

            migrationBuilder.DropColumn(
                name: "FlightIds",
                table: "TourFlights");

            migrationBuilder.CreateTable(
                name: "TourFlightRestrictions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TourFlightId = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    ReferenceId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourFlightRestrictions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourFlightRestrictions_TourFlights_TourFlightId",
                        column: x => x.TourFlightId,
                        principalTable: "TourFlights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TourFlightRestrictions_TourFlightId_Type_ReferenceId",
                table: "TourFlightRestrictions",
                columns: new[] { "TourFlightId", "Type", "ReferenceId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TourFlightRestrictions_Type_ReferenceId",
                table: "TourFlightRestrictions",
                columns: new[] { "Type", "ReferenceId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TourFlightRestrictions");

            migrationBuilder.AddColumn<string>(
                name: "AirlineIds",
                table: "TourFlights",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ArrivalAirportIds",
                table: "TourFlights",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ArrivalCityIds",
                table: "TourFlights",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ArrivalCountryIds",
                table: "TourFlights",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DepartureAirportIds",
                table: "TourFlights",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DepartureCityIds",
                table: "TourFlights",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DepartureCountryIds",
                table: "TourFlights",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FareIds",
                table: "TourFlights",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FlightIds",
                table: "TourFlights",
                type: "text",
                nullable: true);
        }
    }
}
