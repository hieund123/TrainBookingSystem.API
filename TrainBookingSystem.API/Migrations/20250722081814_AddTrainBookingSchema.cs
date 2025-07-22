using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TrainBookingSystem.API.Migrations
{
    /// <inheritdoc />
    public partial class AddTrainBookingSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cc473443-7976-4fb6-9e93-964f96ec4741");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e93c48e4-4341-4740-8a14-ac468210c010");

            migrationBuilder.CreateTable(
                name: "BookingStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarriageClasses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarriageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SeatingCapacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarriageClasses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrainStations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StationName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainStations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarriagePrices",
                columns: table => new
                {
                    ScheduleId = table.Column<int>(type: "int", nullable: false),
                    CarriageClassId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarriagePrices", x => new { x.ScheduleId, x.CarriageClassId });
                    table.ForeignKey(
                        name: "FK_CarriagePrices_CarriageClasses_CarriageClassId",
                        column: x => x.CarriageClassId,
                        principalTable: "CarriageClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarriagePrices_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainJourneys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScheduleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainJourneys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainJourneys_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BookingStatusId = table.Column<int>(type: "int", nullable: false),
                    StartingTrainStationId = table.Column<int>(type: "int", nullable: false),
                    EndingTrainStationId = table.Column<int>(type: "int", nullable: false),
                    TrainJourneyId = table.Column<int>(type: "int", nullable: false),
                    CarriageClassId = table.Column<int>(type: "int", nullable: false),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TicketNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SeatNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AmountPaid = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bookings_BookingStatuses_BookingStatusId",
                        column: x => x.BookingStatusId,
                        principalTable: "BookingStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_CarriageClasses_CarriageClassId",
                        column: x => x.CarriageClassId,
                        principalTable: "CarriageClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_TrainJourneys_TrainJourneyId",
                        column: x => x.TrainJourneyId,
                        principalTable: "TrainJourneys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_TrainStations_EndingTrainStationId",
                        column: x => x.EndingTrainStationId,
                        principalTable: "TrainStations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bookings_TrainStations_StartingTrainStationId",
                        column: x => x.StartingTrainStationId,
                        principalTable: "TrainStations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JourneyCarriages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainJourneyId = table.Column<int>(type: "int", nullable: false),
                    CarriageClassId = table.Column<int>(type: "int", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JourneyCarriages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JourneyCarriages_CarriageClasses_CarriageClassId",
                        column: x => x.CarriageClassId,
                        principalTable: "CarriageClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JourneyCarriages_TrainJourneys_TrainJourneyId",
                        column: x => x.TrainJourneyId,
                        principalTable: "TrainJourneys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JourneyStations",
                columns: table => new
                {
                    TrainStationId = table.Column<int>(type: "int", nullable: false),
                    TrainJourneyId = table.Column<int>(type: "int", nullable: false),
                    StopOrder = table.Column<int>(type: "int", nullable: false),
                    DepartureTime = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JourneyStations", x => new { x.TrainStationId, x.TrainJourneyId });
                    table.ForeignKey(
                        name: "FK_JourneyStations_TrainJourneys_TrainJourneyId",
                        column: x => x.TrainJourneyId,
                        principalTable: "TrainJourneys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JourneyStations_TrainStations_TrainStationId",
                        column: x => x.TrainStationId,
                        principalTable: "TrainStations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "075a38bc-e7b0-4539-87ce-1e0625995c62", "2", "Passenger", "PASSENGER" },
                    { "4642dcf3-2314-4fca-a59e-77f3dec3d9e1", "1", "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_BookingStatusId",
                table: "Bookings",
                column: "BookingStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_CarriageClassId",
                table: "Bookings",
                column: "CarriageClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_EndingTrainStationId",
                table: "Bookings",
                column: "EndingTrainStationId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_StartingTrainStationId",
                table: "Bookings",
                column: "StartingTrainStationId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_TrainJourneyId",
                table: "Bookings",
                column: "TrainJourneyId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserId",
                table: "Bookings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CarriagePrices_CarriageClassId",
                table: "CarriagePrices",
                column: "CarriageClassId");

            migrationBuilder.CreateIndex(
                name: "IX_JourneyCarriages_CarriageClassId",
                table: "JourneyCarriages",
                column: "CarriageClassId");

            migrationBuilder.CreateIndex(
                name: "IX_JourneyCarriages_TrainJourneyId",
                table: "JourneyCarriages",
                column: "TrainJourneyId");

            migrationBuilder.CreateIndex(
                name: "IX_JourneyStations_TrainJourneyId",
                table: "JourneyStations",
                column: "TrainJourneyId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainJourneys_ScheduleId",
                table: "TrainJourneys",
                column: "ScheduleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "CarriagePrices");

            migrationBuilder.DropTable(
                name: "JourneyCarriages");

            migrationBuilder.DropTable(
                name: "JourneyStations");

            migrationBuilder.DropTable(
                name: "BookingStatuses");

            migrationBuilder.DropTable(
                name: "CarriageClasses");

            migrationBuilder.DropTable(
                name: "TrainJourneys");

            migrationBuilder.DropTable(
                name: "TrainStations");

            migrationBuilder.DropTable(
                name: "Schedules");

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
                    { "cc473443-7976-4fb6-9e93-964f96ec4741", "2", "Passenger", "PASSENGER" },
                    { "e93c48e4-4341-4740-8a14-ac468210c010", "1", "Admin", "ADMIN" }
                });
        }
    }
}
