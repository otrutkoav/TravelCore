using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TourCore.Infrastructure.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class AddTourBusTransfers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TourBusTransfers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TourServiceId = table.Column<int>(type: "integer", nullable: false),
                    DepartureTimeFrom = table.Column<TimeSpan>(type: "time without time zone", nullable: false),
                    DepartureTimeTo = table.Column<TimeSpan>(type: "time without time zone", nullable: false),
                    ArrivalTimeFrom = table.Column<TimeSpan>(type: "time without time zone", nullable: false),
                    ArrivalTimeTo = table.Column<TimeSpan>(type: "time without time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourBusTransfers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourBusTransfers_TourServices_TourServiceId",
                        column: x => x.TourServiceId,
                        principalTable: "TourServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TourBusTransferArrivalCities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TourBusTransferId = table.Column<int>(type: "integer", nullable: false),
                    CityId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourBusTransferArrivalCities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourBusTransferArrivalCities_TourBusTransfers_TourBusTransf~",
                        column: x => x.TourBusTransferId,
                        principalTable: "TourBusTransfers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TourBusTransferArrivalCountries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TourBusTransferId = table.Column<int>(type: "integer", nullable: false),
                    CountryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourBusTransferArrivalCountries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourBusTransferArrivalCountries_TourBusTransfers_TourBusTra~",
                        column: x => x.TourBusTransferId,
                        principalTable: "TourBusTransfers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TourBusTransferBoardingPoints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TourBusTransferId = table.Column<int>(type: "integer", nullable: false),
                    BoardingPointId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourBusTransferBoardingPoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourBusTransferBoardingPoints_TourBusTransfers_TourBusTrans~",
                        column: x => x.TourBusTransferId,
                        principalTable: "TourBusTransfers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TourBusTransferDepartureCities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TourBusTransferId = table.Column<int>(type: "integer", nullable: false),
                    CityId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourBusTransferDepartureCities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourBusTransferDepartureCities_TourBusTransfers_TourBusTran~",
                        column: x => x.TourBusTransferId,
                        principalTable: "TourBusTransfers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TourBusTransferDepartureCountries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TourBusTransferId = table.Column<int>(type: "integer", nullable: false),
                    CountryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourBusTransferDepartureCountries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourBusTransferDepartureCountries_TourBusTransfers_TourBusT~",
                        column: x => x.TourBusTransferId,
                        principalTable: "TourBusTransfers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TourBusTransferOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TourBusTransferId = table.Column<int>(type: "integer", nullable: false),
                    BusTransferId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourBusTransferOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourBusTransferOptions_TourBusTransfers_TourBusTransferId",
                        column: x => x.TourBusTransferId,
                        principalTable: "TourBusTransfers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TourBusTransferTransports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TourBusTransferId = table.Column<int>(type: "integer", nullable: false),
                    TransportId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourBusTransferTransports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourBusTransferTransports_TourBusTransfers_TourBusTransferId",
                        column: x => x.TourBusTransferId,
                        principalTable: "TourBusTransfers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TourBusTransferArrivalCities_CityId",
                table: "TourBusTransferArrivalCities",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_TourBusTransferArrivalCities_TourBusTransferId_CityId",
                table: "TourBusTransferArrivalCities",
                columns: new[] { "TourBusTransferId", "CityId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TourBusTransferArrivalCountries_CountryId",
                table: "TourBusTransferArrivalCountries",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_TourBusTransferArrivalCountries_TourBusTransferId_CountryId",
                table: "TourBusTransferArrivalCountries",
                columns: new[] { "TourBusTransferId", "CountryId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TourBusTransferBoardingPoints_BoardingPointId",
                table: "TourBusTransferBoardingPoints",
                column: "BoardingPointId");

            migrationBuilder.CreateIndex(
                name: "IX_TourBusTransferBoardingPoints_TourBusTransferId_BoardingPoi~",
                table: "TourBusTransferBoardingPoints",
                columns: new[] { "TourBusTransferId", "BoardingPointId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TourBusTransferDepartureCities_CityId",
                table: "TourBusTransferDepartureCities",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_TourBusTransferDepartureCities_TourBusTransferId_CityId",
                table: "TourBusTransferDepartureCities",
                columns: new[] { "TourBusTransferId", "CityId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TourBusTransferDepartureCountries_CountryId",
                table: "TourBusTransferDepartureCountries",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_TourBusTransferDepartureCountries_TourBusTransferId_Country~",
                table: "TourBusTransferDepartureCountries",
                columns: new[] { "TourBusTransferId", "CountryId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TourBusTransferOptions_BusTransferId",
                table: "TourBusTransferOptions",
                column: "BusTransferId");

            migrationBuilder.CreateIndex(
                name: "IX_TourBusTransferOptions_TourBusTransferId_BusTransferId",
                table: "TourBusTransferOptions",
                columns: new[] { "TourBusTransferId", "BusTransferId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TourBusTransfers_TourServiceId",
                table: "TourBusTransfers",
                column: "TourServiceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TourBusTransferTransports_TourBusTransferId_TransportId",
                table: "TourBusTransferTransports",
                columns: new[] { "TourBusTransferId", "TransportId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TourBusTransferTransports_TransportId",
                table: "TourBusTransferTransports",
                column: "TransportId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TourBusTransferArrivalCities");

            migrationBuilder.DropTable(
                name: "TourBusTransferArrivalCountries");

            migrationBuilder.DropTable(
                name: "TourBusTransferBoardingPoints");

            migrationBuilder.DropTable(
                name: "TourBusTransferDepartureCities");

            migrationBuilder.DropTable(
                name: "TourBusTransferDepartureCountries");

            migrationBuilder.DropTable(
                name: "TourBusTransferOptions");

            migrationBuilder.DropTable(
                name: "TourBusTransferTransports");

            migrationBuilder.DropTable(
                name: "TourBusTransfers");
        }
    }
}
