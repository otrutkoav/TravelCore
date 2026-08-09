using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TourCore.Infrastructure.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class AddTourHotels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TourHotelGroups",
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
                    table.PrimaryKey("PK_TourHotelGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourHotelGroups_TourServices_TourServiceId",
                        column: x => x.TourServiceId,
                        principalTable: "TourServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TourHotels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TourHotelGroupId = table.Column<int>(type: "integer", nullable: false),
                    OrderIndex = table.Column<int>(type: "integer", nullable: false),
                    CountryId = table.Column<int>(type: "integer", nullable: false),
                    CityId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourHotels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourHotels_TourHotelGroups_TourHotelGroupId",
                        column: x => x.TourHotelGroupId,
                        principalTable: "TourHotelGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TourHotelAllowedHotels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TourHotelId = table.Column<int>(type: "integer", nullable: false),
                    HotelId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourHotelAllowedHotels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourHotelAllowedHotels_TourHotels_TourHotelId",
                        column: x => x.TourHotelId,
                        principalTable: "TourHotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TourHotelDurations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TourHotelId = table.Column<int>(type: "integer", nullable: false),
                    Nights = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourHotelDurations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourHotelDurations_TourHotels_TourHotelId",
                        column: x => x.TourHotelId,
                        principalTable: "TourHotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TourHotelAllowedHotels_HotelId",
                table: "TourHotelAllowedHotels",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_TourHotelAllowedHotels_TourHotelId_HotelId",
                table: "TourHotelAllowedHotels",
                columns: new[] { "TourHotelId", "HotelId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TourHotelDurations_TourHotelId_Nights",
                table: "TourHotelDurations",
                columns: new[] { "TourHotelId", "Nights" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TourHotelGroups_TourServiceId_OrderIndex",
                table: "TourHotelGroups",
                columns: new[] { "TourServiceId", "OrderIndex" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TourHotels_CountryId_CityId",
                table: "TourHotels",
                columns: new[] { "CountryId", "CityId" });

            migrationBuilder.CreateIndex(
                name: "IX_TourHotels_TourHotelGroupId_OrderIndex",
                table: "TourHotels",
                columns: new[] { "TourHotelGroupId", "OrderIndex" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TourHotelAllowedHotels");

            migrationBuilder.DropTable(
                name: "TourHotelDurations");

            migrationBuilder.DropTable(
                name: "TourHotels");

            migrationBuilder.DropTable(
                name: "TourHotelGroups");
        }
    }
}
