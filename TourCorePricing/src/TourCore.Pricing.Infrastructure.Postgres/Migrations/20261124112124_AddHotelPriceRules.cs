using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TourCore.Pricing.Infrastructure.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class AddHotelPriceRules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HotelMealPriceRules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PriceBlockId = table.Column<int>(type: "integer", nullable: false),
                    HotelId = table.Column<int>(type: "integer", nullable: false),
                    RoomTypeId = table.Column<int>(type: "integer", nullable: true),
                    RoomCategoryId = table.Column<int>(type: "integer", nullable: true),
                    AccommodationTypeId = table.Column<int>(type: "integer", nullable: true),
                    FromMealTypeId = table.Column<int>(type: "integer", nullable: true),
                    ToMealTypeId = table.Column<int>(type: "integer", nullable: false),
                    StayDateFrom = table.Column<DateOnly>(type: "date", nullable: false),
                    StayDateTo = table.Column<DateOnly>(type: "date", nullable: false),
                    CheckInDateFrom = table.Column<DateOnly>(type: "date", nullable: true),
                    CheckInDateTo = table.Column<DateOnly>(type: "date", nullable: true),
                    DaysOfWeekMask = table.Column<byte>(type: "smallint", nullable: false),
                    GuestType = table.Column<byte>(type: "smallint", nullable: true),
                    PlacementType = table.Column<byte>(type: "smallint", nullable: false),
                    AgeFrom = table.Column<short>(type: "smallint", nullable: true),
                    AgeTo = table.Column<short>(type: "smallint", nullable: true),
                    ApplicationUnit = table.Column<byte>(type: "smallint", nullable: false),
                    Operation = table.Column<byte>(type: "smallint", nullable: false),
                    NetAmount = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: true),
                    GrossAmount = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: true),
                    Percent = table.Column<decimal>(type: "numeric(9,4)", precision: 9, scale: 4, nullable: true),
                    PercentageBase = table.Column<byte>(type: "smallint", nullable: true),
                    CurrencyId = table.Column<int>(type: "integer", nullable: true),
                    Priority = table.Column<int>(type: "integer", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelMealPriceRules", x => x.Id);
                    table.CheckConstraint("CK_HotelMealPriceRules_Ages", "(\"AgeFrom\" IS NULL OR \"AgeFrom\" >= 0) AND (\"AgeTo\" IS NULL OR \"AgeTo\" >= 0) AND (\"AgeFrom\" IS NULL OR \"AgeTo\" IS NULL OR \"AgeFrom\" <= \"AgeTo\")");
                    table.CheckConstraint("CK_HotelMealPriceRules_Amounts", "(\"NetAmount\" IS NULL OR \"NetAmount\" >= 0) AND (\"GrossAmount\" IS NULL OR \"GrossAmount\" >= 0)");
                    table.CheckConstraint("CK_HotelMealPriceRules_CheckInDates", "(\"CheckInDateFrom\" IS NULL AND \"CheckInDateTo\" IS NULL) OR (\"CheckInDateFrom\" <= \"CheckInDateTo\")");
                    table.CheckConstraint("CK_HotelMealPriceRules_DaysOfWeek", "\"DaysOfWeekMask\" BETWEEN 1 AND 127");
                    table.CheckConstraint("CK_HotelMealPriceRules_Meals", "\"FromMealTypeId\" IS NULL OR \"FromMealTypeId\" <> \"ToMealTypeId\"");
                    table.CheckConstraint("CK_HotelMealPriceRules_Priority", "\"Priority\" >= 0");
                    table.CheckConstraint("CK_HotelMealPriceRules_StayDates", "\"StayDateFrom\" <= \"StayDateTo\"");
                    table.ForeignKey(
                        name: "FK_HotelMealPriceRules_PriceBlocks_PriceBlockId",
                        column: x => x.PriceBlockId,
                        principalTable: "PriceBlocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HotelPriceRules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PriceBlockId = table.Column<int>(type: "integer", nullable: false),
                    HotelId = table.Column<int>(type: "integer", nullable: false),
                    RoomTypeId = table.Column<int>(type: "integer", nullable: false),
                    RoomCategoryId = table.Column<int>(type: "integer", nullable: false),
                    AccommodationTypeId = table.Column<int>(type: "integer", nullable: true),
                    MealTypeId = table.Column<int>(type: "integer", nullable: true),
                    Model = table.Column<byte>(type: "smallint", nullable: false),
                    Priority = table.Column<int>(type: "integer", nullable: false),
                    StayDateFrom = table.Column<DateOnly>(type: "date", nullable: false),
                    StayDateTo = table.Column<DateOnly>(type: "date", nullable: false),
                    CheckInDateFrom = table.Column<DateOnly>(type: "date", nullable: true),
                    CheckInDateTo = table.Column<DateOnly>(type: "date", nullable: true),
                    DaysOfWeekMask = table.Column<byte>(type: "smallint", nullable: false),
                    MinNights = table.Column<short>(type: "smallint", nullable: true),
                    MaxNights = table.Column<short>(type: "smallint", nullable: true),
                    AgeCalculationDate = table.Column<byte>(type: "smallint", nullable: false),
                    ApplicationUnit = table.Column<byte>(type: "smallint", nullable: false),
                    NetAmount = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: true),
                    GrossAmount = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: true),
                    CurrencyId = table.Column<int>(type: "integer", nullable: true),
                    CreatedByUserId = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelPriceRules", x => x.Id);
                    table.CheckConstraint("CK_HotelPriceRules_Amounts", "(\"NetAmount\" IS NULL OR \"NetAmount\" >= 0) AND (\"GrossAmount\" IS NULL OR \"GrossAmount\" >= 0)");
                    table.CheckConstraint("CK_HotelPriceRules_CheckInDates", "(\"CheckInDateFrom\" IS NULL AND \"CheckInDateTo\" IS NULL) OR (\"CheckInDateFrom\" <= \"CheckInDateTo\")");
                    table.CheckConstraint("CK_HotelPriceRules_DaysOfWeek", "\"DaysOfWeekMask\" BETWEEN 1 AND 127");
                    table.CheckConstraint("CK_HotelPriceRules_Nights", "(\"MinNights\" IS NULL OR \"MinNights\" > 0) AND (\"MaxNights\" IS NULL OR \"MaxNights\" > 0) AND (\"MinNights\" IS NULL OR \"MaxNights\" IS NULL OR \"MinNights\" <= \"MaxNights\")");
                    table.CheckConstraint("CK_HotelPriceRules_Priority", "\"Priority\" >= 0");
                    table.CheckConstraint("CK_HotelPriceRules_StayDates", "\"StayDateFrom\" <= \"StayDateTo\"");
                    table.ForeignKey(
                        name: "FK_HotelPriceRules_PriceBlocks_PriceBlockId",
                        column: x => x.PriceBlockId,
                        principalTable: "PriceBlocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HotelPriceComponents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HotelPriceRuleId = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<byte>(type: "smallint", nullable: false),
                    GuestType = table.Column<byte>(type: "smallint", nullable: true),
                    PlacementType = table.Column<byte>(type: "smallint", nullable: false),
                    AgeFrom = table.Column<short>(type: "smallint", nullable: true),
                    AgeTo = table.Column<short>(type: "smallint", nullable: true),
                    Operation = table.Column<byte>(type: "smallint", nullable: false),
                    ApplicationUnit = table.Column<byte>(type: "smallint", nullable: false),
                    NetAmount = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: true),
                    GrossAmount = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: true),
                    Percent = table.Column<decimal>(type: "numeric(9,4)", precision: 9, scale: 4, nullable: true),
                    PercentageBase = table.Column<byte>(type: "smallint", nullable: true),
                    CreatedByUserId = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelPriceComponents", x => x.Id);
                    table.CheckConstraint("CK_HotelPriceComponents_Ages", "(\"AgeFrom\" IS NULL OR \"AgeFrom\" >= 0) AND (\"AgeTo\" IS NULL OR \"AgeTo\" >= 0) AND (\"AgeFrom\" IS NULL OR \"AgeTo\" IS NULL OR \"AgeFrom\" <= \"AgeTo\")");
                    table.CheckConstraint("CK_HotelPriceComponents_Amounts", "(\"NetAmount\" IS NULL OR \"NetAmount\" >= 0) AND (\"GrossAmount\" IS NULL OR \"GrossAmount\" >= 0)");
                    table.CheckConstraint("CK_HotelPriceComponents_Percent", "\"Percent\" IS NULL OR (\"Percent\" > 0 AND \"Percent\" <= 1000)");
                    table.ForeignKey(
                        name: "FK_HotelPriceComponents_HotelPriceRules_HotelPriceRuleId",
                        column: x => x.HotelPriceRuleId,
                        principalTable: "HotelPriceRules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HotelMealPriceRules_Block_Hotel",
                table: "HotelMealPriceRules",
                columns: new[] { "PriceBlockId", "HotelId" });

            migrationBuilder.CreateIndex(
                name: "IX_HotelMealPriceRules_Search",
                table: "HotelMealPriceRules",
                columns: new[] { "HotelId", "FromMealTypeId", "ToMealTypeId", "StayDateFrom", "StayDateTo" });

            migrationBuilder.CreateIndex(
                name: "IX_HotelPriceComponents_RuleId",
                table: "HotelPriceComponents",
                column: "HotelPriceRuleId");

            migrationBuilder.CreateIndex(
                name: "IX_HotelPriceRules_Block_Hotel",
                table: "HotelPriceRules",
                columns: new[] { "PriceBlockId", "HotelId" });

            migrationBuilder.CreateIndex(
                name: "IX_HotelPriceRules_Search",
                table: "HotelPriceRules",
                columns: new[] { "HotelId", "RoomTypeId", "RoomCategoryId", "StayDateFrom", "StayDateTo" });

            migrationBuilder.AddForeignKey(name: "FK_HotelPriceRules_Hotels_HotelId", table: "HotelPriceRules", column: "HotelId", principalTable: "Hotels", principalColumn: "Id", onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(name: "FK_HotelPriceRules_RoomTypes_RoomTypeId", table: "HotelPriceRules", column: "RoomTypeId", principalTable: "RoomTypes", principalColumn: "Id", onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(name: "FK_HotelPriceRules_RoomCategories_RoomCategoryId", table: "HotelPriceRules", column: "RoomCategoryId", principalTable: "RoomCategories", principalColumn: "Id", onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(name: "FK_HotelPriceRules_AccommodationTypes_AccommodationTypeId", table: "HotelPriceRules", column: "AccommodationTypeId", principalTable: "AccommodationTypes", principalColumn: "Id", onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(name: "FK_HotelPriceRules_MealTypes_MealTypeId", table: "HotelPriceRules", column: "MealTypeId", principalTable: "MealTypes", principalColumn: "Id", onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(name: "FK_HotelMealPriceRules_Hotels_HotelId", table: "HotelMealPriceRules", column: "HotelId", principalTable: "Hotels", principalColumn: "Id", onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(name: "FK_HotelMealPriceRules_RoomTypes_RoomTypeId", table: "HotelMealPriceRules", column: "RoomTypeId", principalTable: "RoomTypes", principalColumn: "Id", onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(name: "FK_HotelMealPriceRules_RoomCategories_RoomCategoryId", table: "HotelMealPriceRules", column: "RoomCategoryId", principalTable: "RoomCategories", principalColumn: "Id", onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(name: "FK_HotelMealPriceRules_AccommodationTypes_AccommodationTypeId", table: "HotelMealPriceRules", column: "AccommodationTypeId", principalTable: "AccommodationTypes", principalColumn: "Id", onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(name: "FK_HotelMealPriceRules_MealTypes_FromMealTypeId", table: "HotelMealPriceRules", column: "FromMealTypeId", principalTable: "MealTypes", principalColumn: "Id", onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(name: "FK_HotelMealPriceRules_MealTypes_ToMealTypeId", table: "HotelMealPriceRules", column: "ToMealTypeId", principalTable: "MealTypes", principalColumn: "Id", onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HotelMealPriceRules");

            migrationBuilder.DropTable(
                name: "HotelPriceComponents");

            migrationBuilder.DropTable(
                name: "HotelPriceRules");
        }
    }
}
