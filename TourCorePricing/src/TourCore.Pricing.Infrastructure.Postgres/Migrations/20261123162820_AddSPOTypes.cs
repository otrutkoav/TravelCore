using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourCore.Pricing.Infrastructure.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class AddSPOTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SPOTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    NameLat = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SPOTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "UX_SPOTypes_Name",
                table: "SPOTypes",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SPOTypes");
        }
    }
}
