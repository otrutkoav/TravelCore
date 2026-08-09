using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourCore.Infrastructure.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class RenameTourTourTypeColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TourTourTypes_TourTypes_TourTypesId",
                table: "TourTourTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_TourTourTypes_Tours_ToursId",
                table: "TourTourTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TourTourTypes",
                table: "TourTourTypes");

            migrationBuilder.RenameColumn(
                name: "TourTypesId",
                table: "TourTourTypes",
                newName: "TourTypeId");

            migrationBuilder.RenameColumn(
                name: "ToursId",
                table: "TourTourTypes",
                newName: "TourId");

            migrationBuilder.RenameIndex(
                name: "IX_TourTourTypes_ToursId",
                table: "TourTourTypes",
                newName: "IX_TourTourTypes_TourId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TourTourTypes",
                table: "TourTourTypes",
                columns: new[] { "TourId", "TourTypeId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TourTourTypes_TourTypes_TourTypeId",
                table: "TourTourTypes",
                column: "TourTypeId",
                principalTable: "TourTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TourTourTypes_Tours_TourId",
                table: "TourTourTypes",
                column: "TourId",
                principalTable: "Tours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TourTourTypes_TourTypes_TourTypeId",
                table: "TourTourTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_TourTourTypes_Tours_TourId",
                table: "TourTourTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TourTourTypes",
                table: "TourTourTypes");

            migrationBuilder.RenameColumn(
                name: "TourTypeId",
                table: "TourTourTypes",
                newName: "TourTypesId");

            migrationBuilder.RenameColumn(
                name: "TourId",
                table: "TourTourTypes",
                newName: "ToursId");

            migrationBuilder.RenameIndex(
                name: "IX_TourTourTypes_TourId",
                table: "TourTourTypes",
                newName: "IX_TourTourTypes_ToursId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TourTourTypes",
                table: "TourTourTypes",
                columns: new[] { "TourTypesId", "ToursId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TourTourTypes_TourTypes_TourTypesId",
                table: "TourTourTypes",
                column: "TourTypesId",
                principalTable: "TourTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TourTourTypes_Tours_ToursId",
                table: "TourTourTypes",
                column: "ToursId",
                principalTable: "Tours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
