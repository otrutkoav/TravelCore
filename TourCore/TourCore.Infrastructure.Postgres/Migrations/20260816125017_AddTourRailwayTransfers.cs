using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TourCore.Infrastructure.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class AddTourRailwayTransfers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TourRailwayTransfers",
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
                    table.PrimaryKey("PK_TourRailwayTransfers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourRailwayTransfers_TourServices_TourServiceId",
                        column: x => x.TourServiceId,
                        principalTable: "TourServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TourRailwayTransferArrivalCities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TourRailwayTransferId = table.Column<int>(type: "integer", nullable: false),
                    CityId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourRailwayTransferArrivalCities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourRailwayTransferArrivalCities_TourRailwayTransfers_TourR~",
                        column: x => x.TourRailwayTransferId,
                        principalTable: "TourRailwayTransfers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TourRailwayTransferArrivalCountries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TourRailwayTransferId = table.Column<int>(type: "integer", nullable: false),
                    CountryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourRailwayTransferArrivalCountries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourRailwayTransferArrivalCountries_TourRailwayTransfers_To~",
                        column: x => x.TourRailwayTransferId,
                        principalTable: "TourRailwayTransfers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TourRailwayTransferDepartureCities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TourRailwayTransferId = table.Column<int>(type: "integer", nullable: false),
                    CityId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourRailwayTransferDepartureCities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourRailwayTransferDepartureCities_TourRailwayTransfers_Tou~",
                        column: x => x.TourRailwayTransferId,
                        principalTable: "TourRailwayTransfers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TourRailwayTransferDepartureCountries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TourRailwayTransferId = table.Column<int>(type: "integer", nullable: false),
                    CountryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourRailwayTransferDepartureCountries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourRailwayTransferDepartureCountries_TourRailwayTransfers_~",
                        column: x => x.TourRailwayTransferId,
                        principalTable: "TourRailwayTransfers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TourRailwayTransferOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TourRailwayTransferId = table.Column<int>(type: "integer", nullable: false),
                    RailwayTransferId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourRailwayTransferOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourRailwayTransferOptions_TourRailwayTransfers_TourRailway~",
                        column: x => x.TourRailwayTransferId,
                        principalTable: "TourRailwayTransfers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TourRailwayTransferTransports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TourRailwayTransferId = table.Column<int>(type: "integer", nullable: false),
                    TransportId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourRailwayTransferTransports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourRailwayTransferTransports_TourRailwayTransfers_TourRail~",
                        column: x => x.TourRailwayTransferId,
                        principalTable: "TourRailwayTransfers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TourRailwayTransferArrivalCities_CityId",
                table: "TourRailwayTransferArrivalCities",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_TourRailwayTransferArrivalCities_TourRailwayTransferId_City~",
                table: "TourRailwayTransferArrivalCities",
                columns: new[] { "TourRailwayTransferId", "CityId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TourRailwayTransferArrivalCountries_CountryId",
                table: "TourRailwayTransferArrivalCountries",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_TourRailwayTransferArrivalCountries_TourRailwayTransferId_C~",
                table: "TourRailwayTransferArrivalCountries",
                columns: new[] { "TourRailwayTransferId", "CountryId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TourRailwayTransferDepartureCities_CityId",
                table: "TourRailwayTransferDepartureCities",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_TourRailwayTransferDepartureCities_TourRailwayTransferId_Ci~",
                table: "TourRailwayTransferDepartureCities",
                columns: new[] { "TourRailwayTransferId", "CityId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TourRailwayTransferDepartureCountries_CountryId",
                table: "TourRailwayTransferDepartureCountries",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_TourRailwayTransferDepartureCountries_TourRailwayTransferId~",
                table: "TourRailwayTransferDepartureCountries",
                columns: new[] { "TourRailwayTransferId", "CountryId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TourRailwayTransferOptions_RailwayTransferId",
                table: "TourRailwayTransferOptions",
                column: "RailwayTransferId");

            migrationBuilder.CreateIndex(
                name: "IX_TourRailwayTransferOptions_TourRailwayTransferId_RailwayTra~",
                table: "TourRailwayTransferOptions",
                columns: new[] { "TourRailwayTransferId", "RailwayTransferId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TourRailwayTransfers_TourServiceId",
                table: "TourRailwayTransfers",
                column: "TourServiceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TourRailwayTransferTransports_TourRailwayTransferId_Transpo~",
                table: "TourRailwayTransferTransports",
                columns: new[] { "TourRailwayTransferId", "TransportId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TourRailwayTransferTransports_TransportId",
                table: "TourRailwayTransferTransports",
                column: "TransportId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TourRailwayTransferArrivalCities");

            migrationBuilder.DropTable(
                name: "TourRailwayTransferArrivalCountries");

            migrationBuilder.DropTable(
                name: "TourRailwayTransferDepartureCities");

            migrationBuilder.DropTable(
                name: "TourRailwayTransferDepartureCountries");

            migrationBuilder.DropTable(
                name: "TourRailwayTransferOptions");

            migrationBuilder.DropTable(
                name: "TourRailwayTransferTransports");

            migrationBuilder.DropTable(
                name: "TourRailwayTransfers");
        }
    }
}
