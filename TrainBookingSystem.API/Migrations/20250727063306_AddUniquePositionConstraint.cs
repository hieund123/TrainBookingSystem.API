using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TrainBookingSystem.API.Migrations
{
    /// <inheritdoc />
    public partial class AddUniquePositionConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_JourneyCarriages_TrainJourneyId",
                table: "JourneyCarriages");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "075a38bc-e7b0-4539-87ce-1e0625995c62");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4642dcf3-2314-4fca-a59e-77f3dec3d9e1");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "272c0c8c-158b-4bae-956b-05f28c2fcb11", "2", "Passenger", "PASSENGER" },
                    { "34b4a576-514a-4583-b821-893dc16f8a5e", "1", "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_JourneyCarriages_TrainJourneyId_Position",
                table: "JourneyCarriages",
                columns: new[] { "TrainJourneyId", "Position" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_JourneyCarriages_TrainJourneyId_Position",
                table: "JourneyCarriages");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "272c0c8c-158b-4bae-956b-05f28c2fcb11");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "34b4a576-514a-4583-b821-893dc16f8a5e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "075a38bc-e7b0-4539-87ce-1e0625995c62", "2", "Passenger", "PASSENGER" },
                    { "4642dcf3-2314-4fca-a59e-77f3dec3d9e1", "1", "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_JourneyCarriages_TrainJourneyId",
                table: "JourneyCarriages",
                column: "TrainJourneyId");
        }
    }
}
