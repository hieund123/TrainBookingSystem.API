using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TrainBookingSystem.API.Migrations
{
    /// <inheritdoc />
    public partial class AddDepartureTimeToSchedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "89f3c5db-b5e8-44ad-b6b4-90130c02a92f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a52bfb08-ecc5-464a-96ce-746c28d774f7");

            migrationBuilder.AddColumn<DateTime>(
                name: "DepartureDate",
                table: "TrainJourneys",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "DepartureTime",
                table: "Schedules",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a94cd6bf-422c-4265-8ce0-2792797f9cb6", "2", "Passenger", "PASSENGER" },
                    { "dee7ee07-f09c-4bb5-8156-7a7271a98515", "1", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a94cd6bf-422c-4265-8ce0-2792797f9cb6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dee7ee07-f09c-4bb5-8156-7a7271a98515");

            migrationBuilder.DropColumn(
                name: "DepartureDate",
                table: "TrainJourneys");

            migrationBuilder.DropColumn(
                name: "DepartureTime",
                table: "Schedules");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "89f3c5db-b5e8-44ad-b6b4-90130c02a92f", "1", "Admin", "ADMIN" },
                    { "a52bfb08-ecc5-464a-96ce-746c28d774f7", "2", "Passenger", "PASSENGER" }
                });
        }
    }
}
