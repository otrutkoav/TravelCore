using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TourCore.Pricing.Infrastructure.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class AddFlightPricesAndRenameAmounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_HotelPriceRules_Amounts",
                table: "HotelPriceRules");

            migrationBuilder.DropCheckConstraint(
                name: "CK_HotelPriceComponents_Amounts",
                table: "HotelPriceComponents");

            migrationBuilder.DropCheckConstraint(
                name: "CK_HotelMealPriceRules_Amounts",
                table: "HotelMealPriceRules");

            migrationBuilder.RenameColumn(
                name: "NetAmount",
                table: "HotelPriceRules",
                newName: "NettoAmount");

            migrationBuilder.RenameColumn(
                name: "GrossAmount",
                table: "HotelPriceRules",
                newName: "BruttoAmount");

            migrationBuilder.RenameColumn(
                name: "NetAmount",
                table: "HotelPriceComponents",
                newName: "NettoAmount");

            migrationBuilder.RenameColumn(
                name: "GrossAmount",
                table: "HotelPriceComponents",
                newName: "BruttoAmount");

            migrationBuilder.RenameColumn(
                name: "NetAmount",
                table: "HotelMealPriceRules",
                newName: "NettoAmount");

            migrationBuilder.RenameColumn(
                name: "GrossAmount",
                table: "HotelMealPriceRules",
                newName: "BruttoAmount");

            migrationBuilder.CreateTable(
                name: "FlightPrices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PriceBlockId = table.Column<int>(type: "integer", nullable: false),
                    CharterId = table.Column<int>(type: "integer", nullable: false),
                    AirClassId = table.Column<int>(type: "integer", nullable: false),
                    TravelDateFrom = table.Column<DateOnly>(type: "date", nullable: false),
                    TravelDateTo = table.Column<DateOnly>(type: "date", nullable: false),
                    DaysOfWeekMask = table.Column<byte>(type: "smallint", nullable: false),
                    SaleDateFrom = table.Column<DateOnly>(type: "date", nullable: true),
                    SaleDateTo = table.Column<DateOnly>(type: "date", nullable: true),
                    AgeFrom = table.Column<short>(type: "smallint", nullable: true),
                    AgeTo = table.Column<short>(type: "smallint", nullable: true),
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
                    table.PrimaryKey("PK_FlightPrices", x => x.Id);
                    table.CheckConstraint("CK_FlightPrices_Ages", "(\"AgeFrom\" IS NULL OR \"AgeFrom\" >= 0) AND (\"AgeTo\" IS NULL OR \"AgeTo\" >= 0) AND (\"AgeFrom\" IS NULL OR \"AgeTo\" IS NULL OR \"AgeFrom\" <= \"AgeTo\")");
                    table.CheckConstraint("CK_FlightPrices_Amounts", "(\"NettoAmount\" IS NOT NULL OR \"BruttoAmount\" IS NOT NULL) AND (\"NettoAmount\" IS NULL OR \"NettoAmount\" >= 0) AND (\"BruttoAmount\" IS NULL OR \"BruttoAmount\" >= 0)");
                    table.CheckConstraint("CK_FlightPrices_DaysOfWeek", "\"DaysOfWeekMask\" BETWEEN 1 AND 127");
                    table.CheckConstraint("CK_FlightPrices_Priority", "\"Priority\" >= 0");
                    table.CheckConstraint("CK_FlightPrices_SaleDates", "\"SaleDateFrom\" IS NULL OR \"SaleDateTo\" IS NULL OR \"SaleDateFrom\" <= \"SaleDateTo\"");
                    table.CheckConstraint("CK_FlightPrices_TravelDates", "\"TravelDateFrom\" <= \"TravelDateTo\"");
                    table.ForeignKey(
                        name: "FK_FlightPrices_PriceBlocks_PriceBlockId",
                        column: x => x.PriceBlockId,
                        principalTable: "PriceBlocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddCheckConstraint(
                name: "CK_HotelPriceRules_Amounts",
                table: "HotelPriceRules",
                sql: "(\"NettoAmount\" IS NULL OR \"NettoAmount\" >= 0) AND (\"BruttoAmount\" IS NULL OR \"BruttoAmount\" >= 0)");

            migrationBuilder.AddCheckConstraint(
                name: "CK_HotelPriceComponents_Amounts",
                table: "HotelPriceComponents",
                sql: "(\"NettoAmount\" IS NULL OR \"NettoAmount\" >= 0) AND (\"BruttoAmount\" IS NULL OR \"BruttoAmount\" >= 0)");

            migrationBuilder.AddCheckConstraint(
                name: "CK_HotelMealPriceRules_Amounts",
                table: "HotelMealPriceRules",
                sql: "(\"NettoAmount\" IS NULL OR \"NettoAmount\" >= 0) AND (\"BruttoAmount\" IS NULL OR \"BruttoAmount\" >= 0)");

            migrationBuilder.CreateIndex(
                name: "IX_FlightPrices_Block_Charter_AirClass",
                table: "FlightPrices",
                columns: new[] { "PriceBlockId", "CharterId", "AirClassId" });

            migrationBuilder.CreateIndex(
                name: "IX_FlightPrices_Search",
                table: "FlightPrices",
                columns: new[] { "CharterId", "AirClassId", "TravelDateFrom", "TravelDateTo" });

            migrationBuilder.AddForeignKey(
                name: "FK_FlightPrices_Charters_CharterId",
                table: "FlightPrices",
                column: "CharterId",
                principalTable: "Charters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FlightPrices_AirClasses_AirClassId",
                table: "FlightPrices",
                column: "AirClassId",
                principalTable: "AirClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlightPrices");

            migrationBuilder.DropCheckConstraint(
                name: "CK_HotelPriceRules_Amounts",
                table: "HotelPriceRules");

            migrationBuilder.DropCheckConstraint(
                name: "CK_HotelPriceComponents_Amounts",
                table: "HotelPriceComponents");

            migrationBuilder.DropCheckConstraint(
                name: "CK_HotelMealPriceRules_Amounts",
                table: "HotelMealPriceRules");

            migrationBuilder.RenameColumn(
                name: "NettoAmount",
                table: "HotelPriceRules",
                newName: "NetAmount");

            migrationBuilder.RenameColumn(
                name: "BruttoAmount",
                table: "HotelPriceRules",
                newName: "GrossAmount");

            migrationBuilder.RenameColumn(
                name: "NettoAmount",
                table: "HotelPriceComponents",
                newName: "NetAmount");

            migrationBuilder.RenameColumn(
                name: "BruttoAmount",
                table: "HotelPriceComponents",
                newName: "GrossAmount");

            migrationBuilder.RenameColumn(
                name: "NettoAmount",
                table: "HotelMealPriceRules",
                newName: "NetAmount");

            migrationBuilder.RenameColumn(
                name: "BruttoAmount",
                table: "HotelMealPriceRules",
                newName: "GrossAmount");

            migrationBuilder.AddCheckConstraint(
                name: "CK_HotelPriceRules_Amounts",
                table: "HotelPriceRules",
                sql: "(\"NetAmount\" IS NULL OR \"NetAmount\" >= 0) AND (\"GrossAmount\" IS NULL OR \"GrossAmount\" >= 0)");

            migrationBuilder.AddCheckConstraint(
                name: "CK_HotelPriceComponents_Amounts",
                table: "HotelPriceComponents",
                sql: "(\"NetAmount\" IS NULL OR \"NetAmount\" >= 0) AND (\"GrossAmount\" IS NULL OR \"GrossAmount\" >= 0)");

            migrationBuilder.AddCheckConstraint(
                name: "CK_HotelMealPriceRules_Amounts",
                table: "HotelMealPriceRules",
                sql: "(\"NetAmount\" IS NULL OR \"NetAmount\" >= 0) AND (\"GrossAmount\" IS NULL OR \"GrossAmount\" >= 0)");
        }
    }
}
