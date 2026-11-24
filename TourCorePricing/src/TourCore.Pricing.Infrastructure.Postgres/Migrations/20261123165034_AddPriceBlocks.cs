using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TourCore.Pricing.Infrastructure.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class AddPriceBlocks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PriceBlocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PricePackageId = table.Column<int>(type: "integer", nullable: false),
                    SPOTypeId = table.Column<int>(type: "integer", nullable: false),
                    SeasonId = table.Column<int>(type: "integer", nullable: false),
                    ServiceTypeId = table.Column<int>(type: "integer", nullable: false),
                    PartnerId = table.Column<int>(type: "integer", nullable: false),
                    CurrencyId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(254)", maxLength: 254, nullable: false),
                    Code = table.Column<string>(type: "character varying(254)", maxLength: 254, nullable: false),
                    Description = table.Column<string>(type: "character varying(254)", maxLength: 254, nullable: false),
                    Comment = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    OnlineInfoText = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    SaleStartsAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    SaleEndsAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DaysBeforeCheckInFrom = table.Column<int>(type: "integer", nullable: true),
                    DaysBeforeCheckInTo = table.Column<int>(type: "integer", nullable: true),
                    ExcludeFromRecalculation = table.Column<bool>(type: "boolean", nullable: false),
                    State = table.Column<byte>(type: "smallint", nullable: false),
                    SentForReviewAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ActivatedForNetAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ActivatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeactivatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedByUserId = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    SentForReviewByUserId = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ActivatedForNetByUserId = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ActivatedByUserId = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    DeactivatedByUserId = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceBlocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriceBlocks_PricePackages_PricePackageId",
                        column: x => x.PricePackageId,
                        principalTable: "PricePackages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PriceBlocks_SPOTypes_SPOTypeId",
                        column: x => x.SPOTypeId,
                        principalTable: "SPOTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PriceBlocks_PricePackageId",
                table: "PriceBlocks",
                column: "PricePackageId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceBlocks_ServiceTypeId_SaleDates_State",
                table: "PriceBlocks",
                columns: new[] { "ServiceTypeId", "SaleStartsAt", "SaleEndsAt", "State" });

            migrationBuilder.CreateIndex(
                name: "IX_PriceBlocks_SPOTypeId",
                table: "PriceBlocks",
                column: "SPOTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceBlocks_State_SaleEndsAt",
                table: "PriceBlocks",
                columns: new[] { "State", "SaleEndsAt" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PriceBlocks");
        }
    }
}
