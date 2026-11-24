using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TourCore.Pricing.Infrastructure.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class AddSeasons : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Seasons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    NameLat = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsMain = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seasons", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Seasons",
                columns: new[]
                {
                    "Id", "Name", "NameLat", "IsActive", "IsMain",
                    "CreatedByUserId", "CreatedAt", "UpdatedAt"
                },
                values: new object[,]
                {
                    { 1, "<All>", "<All>", true, true, "migration", new DateTime(2013, 6, 10, 13, 13, 15, 403, DateTimeKind.Utc), null },
                    { 2, "Зима", "Winter", true, false, "migration", new DateTime(2016, 12, 13, 11, 6, 39, 240, DateTimeKind.Utc), null },
                    { 3, "Весна", "Spring", true, false, "migration", new DateTime(2017, 6, 13, 12, 10, 55, 930, DateTimeKind.Utc), null },
                    { 4, "Лето", "Summer", true, false, "migration", new DateTime(2017, 6, 13, 12, 12, 28, 810, DateTimeKind.Utc), null },
                    { 5, "Осень", "Autumn", true, false, "migration", new DateTime(2017, 6, 13, 12, 12, 35, 543, DateTimeKind.Utc), null }
                });

            migrationBuilder.Sql("SELECT setval(pg_get_serial_sequence('\"Seasons\"', 'Id'), 5, true);");

            migrationBuilder.CreateIndex(
                name: "IX_PriceBlocks_SeasonId",
                table: "PriceBlocks",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "UX_Seasons_Name",
                table: "Seasons",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UX_Seasons_OnlyOneMain",
                table: "Seasons",
                column: "IsMain",
                unique: true,
                filter: "\"IsMain\"");

            migrationBuilder.AddForeignKey(
                name: "FK_PriceBlocks_Seasons_SeasonId",
                table: "PriceBlocks",
                column: "SeasonId",
                principalTable: "Seasons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PriceBlocks_Seasons_SeasonId",
                table: "PriceBlocks");

            migrationBuilder.DropTable(
                name: "Seasons");

            migrationBuilder.DropIndex(
                name: "IX_PriceBlocks_SeasonId",
                table: "PriceBlocks");
        }
    }
}
