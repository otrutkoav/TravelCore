using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TourCore.Pricing.Infrastructure.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class AddBusAndRailwayTransferPrices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BusTransferPrices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PriceBlockId = table.Column<int>(type: "integer", nullable: false),
                    BusTransferPointId = table.Column<int>(type: "integer", nullable: false),
                    TransportId = table.Column<int>(type: "integer", nullable: false),
                    TravelDateFrom = table.Column<DateOnly>(type: "date", nullable: true),
                    TravelDateTo = table.Column<DateOnly>(type: "date", nullable: true),
                    DaysOfWeekMask = table.Column<byte>(type: "smallint", nullable: false),
                    SaleDateFrom = table.Column<DateOnly>(type: "date", nullable: true),
                    SaleDateTo = table.Column<DateOnly>(type: "date", nullable: true),
                    AgeFrom = table.Column<short>(type: "smallint", nullable: true),
                    AgeTo = table.Column<short>(type: "smallint", nullable: true),
                    PassengerCountFrom = table.Column<short>(type: "smallint", nullable: true),
                    PassengerCountTo = table.Column<short>(type: "smallint", nullable: true),
                    Scope = table.Column<byte>(type: "smallint", nullable: false),
                    PeriodUnit = table.Column<byte>(type: "smallint", nullable: false),
                    NettoAmount = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: true),
                    BruttoAmount = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: true),
                    CurrencyId = table.Column<int>(type: "integer", nullable: true),
                    Priority = table.Column<int>(type: "integer", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusTransferPrices", x => x.Id);
                    table.CheckConstraint("CK_BusTransferPrices_Ages", "(\"AgeFrom\" IS NULL OR \"AgeFrom\" >= 0) AND (\"AgeTo\" IS NULL OR \"AgeTo\" >= 0) AND (\"AgeFrom\" IS NULL OR \"AgeTo\" IS NULL OR \"AgeFrom\" <= \"AgeTo\")");
                    table.CheckConstraint("CK_BusTransferPrices_Amounts", "(\"NettoAmount\" IS NOT NULL OR \"BruttoAmount\" IS NOT NULL) AND (\"NettoAmount\" IS NULL OR \"NettoAmount\" >= 0) AND (\"BruttoAmount\" IS NULL OR \"BruttoAmount\" >= 0)");
                    table.CheckConstraint("CK_BusTransferPrices_Days", "\"DaysOfWeekMask\" BETWEEN 1 AND 127");
                    table.CheckConstraint("CK_BusTransferPrices_Passengers", "(\"PassengerCountFrom\" IS NULL OR \"PassengerCountFrom\" > 0) AND (\"PassengerCountTo\" IS NULL OR \"PassengerCountTo\" > 0) AND (\"PassengerCountFrom\" IS NULL OR \"PassengerCountTo\" IS NULL OR \"PassengerCountFrom\" <= \"PassengerCountTo\")");
                    table.CheckConstraint("CK_BusTransferPrices_Priority", "\"Priority\" >= 0");
                    table.CheckConstraint("CK_BusTransferPrices_SaleDates", "\"SaleDateFrom\" IS NULL OR \"SaleDateTo\" IS NULL OR \"SaleDateFrom\" <= \"SaleDateTo\"");
                    table.CheckConstraint("CK_BusTransferPrices_TravelDates", "\"TravelDateFrom\" IS NULL OR \"TravelDateTo\" IS NULL OR \"TravelDateFrom\" <= \"TravelDateTo\"");
                    table.ForeignKey(
                        name: "FK_BusTransferPrices_PriceBlocks_PriceBlockId",
                        column: x => x.PriceBlockId,
                        principalTable: "PriceBlocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RailwayTransferPrices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PriceBlockId = table.Column<int>(type: "integer", nullable: false),
                    RailwayTransferId = table.Column<int>(type: "integer", nullable: false),
                    TransportId = table.Column<int>(type: "integer", nullable: false),
                    TravelDateFrom = table.Column<DateOnly>(type: "date", nullable: true),
                    TravelDateTo = table.Column<DateOnly>(type: "date", nullable: true),
                    DaysOfWeekMask = table.Column<byte>(type: "smallint", nullable: false),
                    SaleDateFrom = table.Column<DateOnly>(type: "date", nullable: true),
                    SaleDateTo = table.Column<DateOnly>(type: "date", nullable: true),
                    AgeFrom = table.Column<short>(type: "smallint", nullable: true),
                    AgeTo = table.Column<short>(type: "smallint", nullable: true),
                    Scope = table.Column<byte>(type: "smallint", nullable: false),
                    PeriodUnit = table.Column<byte>(type: "smallint", nullable: false),
                    NettoAmount = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: true),
                    BruttoAmount = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: true),
                    CurrencyId = table.Column<int>(type: "integer", nullable: true),
                    Priority = table.Column<int>(type: "integer", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RailwayTransferPrices", x => x.Id);
                    table.CheckConstraint("CK_RailwayTransferPrices_Ages", "(\"AgeFrom\" IS NULL OR \"AgeFrom\" >= 0) AND (\"AgeTo\" IS NULL OR \"AgeTo\" >= 0) AND (\"AgeFrom\" IS NULL OR \"AgeTo\" IS NULL OR \"AgeFrom\" <= \"AgeTo\")");
                    table.CheckConstraint("CK_RailwayTransferPrices_Amounts", "(\"NettoAmount\" IS NOT NULL OR \"BruttoAmount\" IS NOT NULL) AND (\"NettoAmount\" IS NULL OR \"NettoAmount\" >= 0) AND (\"BruttoAmount\" IS NULL OR \"BruttoAmount\" >= 0)");
                    table.CheckConstraint("CK_RailwayTransferPrices_Days", "\"DaysOfWeekMask\" BETWEEN 1 AND 127");
                    table.CheckConstraint("CK_RailwayTransferPrices_Priority", "\"Priority\" >= 0");
                    table.CheckConstraint("CK_RailwayTransferPrices_SaleDates", "\"SaleDateFrom\" IS NULL OR \"SaleDateTo\" IS NULL OR \"SaleDateFrom\" <= \"SaleDateTo\"");
                    table.CheckConstraint("CK_RailwayTransferPrices_TravelDates", "\"TravelDateFrom\" IS NULL OR \"TravelDateTo\" IS NULL OR \"TravelDateFrom\" <= \"TravelDateTo\"");
                    table.ForeignKey(
                        name: "FK_RailwayTransferPrices_PriceBlocks_PriceBlockId",
                        column: x => x.PriceBlockId,
                        principalTable: "PriceBlocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusTransferPrices_Block_Point_Transport",
                table: "BusTransferPrices",
                columns: new[] { "PriceBlockId", "BusTransferPointId", "TransportId" });

            migrationBuilder.CreateIndex(
                name: "IX_BusTransferPrices_Search",
                table: "BusTransferPrices",
                columns: new[] { "BusTransferPointId", "TransportId", "TravelDateFrom", "TravelDateTo" });

            migrationBuilder.CreateIndex(
                name: "IX_RailwayTransferPrices_Block_Transfer_Transport",
                table: "RailwayTransferPrices",
                columns: new[] { "PriceBlockId", "RailwayTransferId", "TransportId" });

            migrationBuilder.CreateIndex(
                name: "IX_RailwayTransferPrices_Search",
                table: "RailwayTransferPrices",
                columns: new[] { "RailwayTransferId", "TransportId", "TravelDateFrom", "TravelDateTo" });

            migrationBuilder.AddForeignKey(name: "FK_BusTransferPrices_BusTransferPoints_BusTransferPointId",
                table: "BusTransferPrices", column: "BusTransferPointId", principalTable: "BusTransferPoints",
                principalColumn: "Id", onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(name: "FK_BusTransferPrices_Transports_TransportId",
                table: "BusTransferPrices", column: "TransportId", principalTable: "Transports",
                principalColumn: "Id", onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(name: "FK_RailwayTransferPrices_RailwayTransfers_RailwayTransferId",
                table: "RailwayTransferPrices", column: "RailwayTransferId", principalTable: "RailwayTransfers",
                principalColumn: "Id", onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(name: "FK_RailwayTransferPrices_Transports_TransportId",
                table: "RailwayTransferPrices", column: "TransportId", principalTable: "Transports",
                principalColumn: "Id", onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusTransferPrices");

            migrationBuilder.DropTable(
                name: "RailwayTransferPrices");
        }
    }
}
