using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourCore.Pricing.Infrastructure.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class AlignPricingNamesWithTourCore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_price_packages",
                table: "price_packages");

            migrationBuilder.DropIndex(
                name: "ux_price_packages_country_name_active",
                table: "price_packages");

            migrationBuilder.RenameTable(
                name: "price_packages",
                newName: "PricePackages");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "PricePackages",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "deleted",
                table: "PricePackages",
                newName: "Deleted");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "PricePackages",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "PricePackages",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "is_disabled",
                table: "PricePackages",
                newName: "IsDisabled");

            migrationBuilder.RenameColumn(
                name: "created_by_user_id",
                table: "PricePackages",
                newName: "CreatedByUserId");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "PricePackages",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "country_id",
                table: "PricePackages",
                newName: "CountryId");

            migrationBuilder.RenameIndex(
                name: "ix_price_packages_country_id",
                table: "PricePackages",
                newName: "IX_PricePackages_CountryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PricePackages",
                table: "PricePackages",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "UX_PricePackages_CountryId_Name_Active",
                table: "PricePackages",
                columns: new[] { "CountryId", "Name" },
                unique: true,
                filter: "NOT \"Deleted\"");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PricePackages",
                table: "PricePackages");

            migrationBuilder.DropIndex(
                name: "UX_PricePackages_CountryId_Name_Active",
                table: "PricePackages");

            migrationBuilder.RenameTable(
                name: "PricePackages",
                newName: "price_packages");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "price_packages",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Deleted",
                table: "price_packages",
                newName: "deleted");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "price_packages",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "price_packages",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "IsDisabled",
                table: "price_packages",
                newName: "is_disabled");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "price_packages",
                newName: "created_by_user_id");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "price_packages",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "CountryId",
                table: "price_packages",
                newName: "country_id");

            migrationBuilder.RenameIndex(
                name: "IX_PricePackages_CountryId",
                table: "price_packages",
                newName: "ix_price_packages_country_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_price_packages",
                table: "price_packages",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "ux_price_packages_country_name_active",
                table: "price_packages",
                columns: new[] { "country_id", "name" },
                unique: true,
                filter: "NOT deleted");
        }
    }
}
