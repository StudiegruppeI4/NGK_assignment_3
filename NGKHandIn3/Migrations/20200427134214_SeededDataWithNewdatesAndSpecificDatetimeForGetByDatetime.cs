using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NGKHandIn3.Migrations
{
    public partial class SeededDataWithNewdatesAndSpecificDatetimeForGetByDatetime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "WeatherObservations",
                keyColumn: "Id",
                keyValue: 1,
                column: "Time",
                value: new DateTime(2009, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "WeatherObservations",
                keyColumn: "Id",
                keyValue: 1,
                column: "Time",
                value: new DateTime(2020, 4, 27, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}
