using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Alaska_Calendar.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "FlightPrices",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "FlightPrices",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "FlightPrices",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "FlightPrices",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "FlightPrices",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "FlightPrices",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "FlightPrices",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "FlightPrices",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "FlightPrices",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "FlightPrices",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "FlightPrices",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "FlightPrices",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "FlightPrices",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "FlightPrices",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "FlightPrices",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "FlightPrices",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "FlightPrices",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "FlightPrices",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "FlightPrices",
                keyColumn: "Id",
                keyValue: 30);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "FlightPrices",
                columns: new[] { "Id", "Date", "Price" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 295m },
                    { 2, new DateTime(2024, 9, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 143m },
                    { 3, new DateTime(2024, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 87m },
                    { 4, new DateTime(2024, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 200m },
                    { 5, new DateTime(2024, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 289m },
                    { 6, new DateTime(2024, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 75m },
                    { 7, new DateTime(2024, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 172m },
                    { 8, new DateTime(2024, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 90m },
                    { 9, new DateTime(2024, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 234m },
                    { 10, new DateTime(2024, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 150m },
                    { 11, new DateTime(2024, 9, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 291m },
                    { 12, new DateTime(2024, 9, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 105m },
                    { 13, new DateTime(2024, 9, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 77m },
                    { 14, new DateTime(2024, 9, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 250m },
                    { 15, new DateTime(2024, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 142m },
                    { 16, new DateTime(2024, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 90m },
                    { 17, new DateTime(2024, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 180m },
                    { 18, new DateTime(2024, 9, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 279m },
                    { 19, new DateTime(2024, 9, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 75m },
                    { 20, new DateTime(2024, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 278m },
                    { 21, new DateTime(2024, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 239m },
                    { 22, new DateTime(2024, 9, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 145m },
                    { 23, new DateTime(2024, 9, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 88m },
                    { 24, new DateTime(2024, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 299m },
                    { 25, new DateTime(2024, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 143m },
                    { 26, new DateTime(2024, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 287m },
                    { 27, new DateTime(2024, 9, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 83m },
                    { 28, new DateTime(2024, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 230m },
                    { 29, new DateTime(2024, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 166m },
                    { 30, new DateTime(2024, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 297m }
                });
        }
    }
}
