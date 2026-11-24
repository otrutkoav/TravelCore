using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourCore.Pricing.Infrastructure.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class AddSPOTypeAudit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "SPOTypes",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "SPOTypes",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "SPOTypes",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.Sql("UPDATE \"SPOTypes\" SET \"CreatedAt\" = CURRENT_TIMESTAMP, \"CreatedByUserId\" = 'migration'");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "SPOTypes",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedByUserId",
                table: "SPOTypes",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "SPOTypes");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "SPOTypes");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "SPOTypes");
        }
    }
}
