using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NGKHandIn3.Migrations
{
    public partial class SeededDataWithNewdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "WeatherObservations",
                keyColumn: "Id",
                keyValue: 2,
                column: "Time",
                value: new DateTime(2020, 4, 17, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "WeatherObservations",
                keyColumn: "Id",
                keyValue: 3,
                column: "Time",
                value: new DateTime(2020, 4, 27, 3, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "WeatherObservations",
                keyColumn: "Id",
                keyValue: 4,
                column: "Time",
                value: new DateTime(2020, 4, 27, 4, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "WeatherObservations",
                keyColumn: "Id",
                keyValue: 5,
                column: "Time",
                value: new DateTime(2020, 4, 27, 5, 0, 0, 0, DateTimeKind.Local));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "WeatherObservations",
                keyColumn: "Id",
                keyValue: 2,
                column: "Time",
                value: new DateTime(2020, 4, 27, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "WeatherObservations",
                keyColumn: "Id",
                keyValue: 3,
                column: "Time",
                value: new DateTime(2020, 4, 27, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "WeatherObservations",
                keyColumn: "Id",
                keyValue: 4,
                column: "Time",
                value: new DateTime(2020, 4, 27, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "WeatherObservations",
                keyColumn: "Id",
                keyValue: 5,
                column: "Time",
                value: new DateTime(2020, 4, 27, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}
