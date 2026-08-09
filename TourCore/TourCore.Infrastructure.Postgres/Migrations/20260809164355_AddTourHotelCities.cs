using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TourCore.Infrastructure.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class AddTourHotelCities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TourHotels_CountryId_CityId",
                table: "TourHotels");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "TourHotels");

            migrationBuilder.CreateTable(
                name: "TourHotelCities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TourHotelId = table.Column<int>(type: "integer", nullable: false),
                    CityId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourHotelCities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourHotelCities_TourHotels_TourHotelId",
                        column: x => x.TourHotelId,
                        principalTable: "TourHotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TourHotels_CountryId",
                table: "TourHotels",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_TourHotelCities_CityId",
                table: "TourHotelCities",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_TourHotelCities_TourHotelId_CityId",
                table: "TourHotelCities",
                columns: new[] { "TourHotelId", "CityId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TourHotelCities");

            migrationBuilder.DropIndex(
                name: "IX_TourHotels_CountryId",
                table: "TourHotels");

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "TourHotels",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TourHotels_CountryId_CityId",
                table: "TourHotels",
                columns: new[] { "CountryId", "CityId" });
        }
    }
}
