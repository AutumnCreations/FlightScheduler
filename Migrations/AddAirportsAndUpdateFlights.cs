using Microsoft.EntityFrameworkCore.Migrations;

public partial class AddAirportsAndUpdateFlights : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Airports",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Abbreviation = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                State = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Airports", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "FlightPrices",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                DepartureTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                ArrivalTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                DepartureAirportId = table.Column<int>(type: "int", nullable: false),
                ArrivalAirportId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_FlightPrices", x => x.Id);
                table.ForeignKey(
                    name: "FK_FlightPrices_Airports_DepartureAirportId",
                    column: x => x.DepartureAirportId,
                    principalTable: "Airports",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_FlightPrices_Airports_ArrivalAirportId",
                    column: x => x.ArrivalAirportId,
                    principalTable: "Airports",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateIndex(
            name: "IX_FlightPrices_DepartureAirportId",
            table: "FlightPrices",
            column: "DepartureAirportId");

        migrationBuilder.CreateIndex(
            name: "IX_FlightPrices_ArrivalAirportId",
            table: "FlightPrices",
            column: "ArrivalAirportId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "FlightPrices");

        migrationBuilder.DropTable(
            name: "Airports");
    }
}