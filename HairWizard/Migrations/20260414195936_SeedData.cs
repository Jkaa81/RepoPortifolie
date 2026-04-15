using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HairWizard.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "Name" },
                values: new object[,]
                {
                    { 1, "Sofie" },
                    { 2, "Emma" },
                    { 3, "Freja" },
                    { 4, "Ida" }
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "BookingId", "ApplicationUserId", "EmployeeId", "EndTime", "StartTime", "Title" },
                values: new object[,]
                {
                    { 1, null, 1, new DateTime(2026, 4, 20, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 4, 20, 9, 0, 0, 0, DateTimeKind.Unspecified), "Haircut" },
                    { 2, null, 2, new DateTime(2026, 4, 20, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 4, 20, 10, 30, 0, 0, DateTimeKind.Unspecified), "Coloring" },
                    { 3, null, 3, new DateTime(2026, 4, 21, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 4, 21, 13, 0, 0, 0, DateTimeKind.Unspecified), "Styling" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "BookingId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "BookingId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "BookingId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 3);
        }
    }
}
