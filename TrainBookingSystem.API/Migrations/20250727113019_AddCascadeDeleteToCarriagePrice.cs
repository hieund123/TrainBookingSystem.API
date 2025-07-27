using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TrainBookingSystem.API.Migrations
{
    /// <inheritdoc />
    public partial class AddCascadeDeleteToCarriagePrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "272c0c8c-158b-4bae-956b-05f28c2fcb11");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "34b4a576-514a-4583-b821-893dc16f8a5e");

            migrationBuilder.AddColumn<int>(
                name: "CarriageClassId1",
                table: "CarriagePrices",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "89f3c5db-b5e8-44ad-b6b4-90130c02a92f", "1", "Admin", "ADMIN" },
                    { "a52bfb08-ecc5-464a-96ce-746c28d774f7", "2", "Passenger", "PASSENGER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarriagePrices_CarriageClassId1",
                table: "CarriagePrices",
                column: "CarriageClassId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CarriagePrices_CarriageClasses_CarriageClassId1",
                table: "CarriagePrices",
                column: "CarriageClassId1",
                principalTable: "CarriageClasses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarriagePrices_CarriageClasses_CarriageClassId1",
                table: "CarriagePrices");

            migrationBuilder.DropIndex(
                name: "IX_CarriagePrices_CarriageClassId1",
                table: "CarriagePrices");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "89f3c5db-b5e8-44ad-b6b4-90130c02a92f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a52bfb08-ecc5-464a-96ce-746c28d774f7");

            migrationBuilder.DropColumn(
                name: "CarriageClassId1",
                table: "CarriagePrices");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "272c0c8c-158b-4bae-956b-05f28c2fcb11", "2", "Passenger", "PASSENGER" },
                    { "34b4a576-514a-4583-b821-893dc16f8a5e", "1", "Admin", "ADMIN" }
                });
        }
    }
}
