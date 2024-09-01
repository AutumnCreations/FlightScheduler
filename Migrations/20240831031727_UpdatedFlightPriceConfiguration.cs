using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Alaska_Calendar.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedFlightPriceConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "FlightPrices",
                columns: new[] { "Id", "Date", "Price" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 295m },
                    { 2, new DateTime(2024, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 143m },
                    { 3, new DateTime(2024, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 87m },
                    { 4, new DateTime(2024, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 200m },
                    { 5, new DateTime(2024, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 289m },
                    { 6, new DateTime(2024, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 75m },
                    { 7, new DateTime(2024, 10, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 172m },
                    { 8, new DateTime(2024, 10, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 90m },
                    { 9, new DateTime(2024, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 234m },
                    { 10, new DateTime(2024, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 150m },
                    { 31, new DateTime(2024, 10, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 85m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FlightPrices",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "FlightPrices",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "FlightPrices",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "FlightPrices",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "FlightPrices",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "FlightPrices",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "FlightPrices",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "FlightPrices",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "FlightPrices",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "FlightPrices",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "FlightPrices",
                keyColumn: "Id",
                keyValue: 31);
        }
    }
}
