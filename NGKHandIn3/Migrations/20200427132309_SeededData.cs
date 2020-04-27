using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NGKHandIn3.Migrations
{
    public partial class SeededData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "WeatherObservations",
                columns: new[] { "Id", "AirPressure", "HumidityPercentage", "Latitude", "LocationName", "Longitude", "Temperature", "Time" },
                values: new object[,]
                {
                    { 1, 2.7999999999999998, 20, 66.700000000000003, "Florida", 90.200000000000003, 30.0, new DateTime(2020, 4, 27, 0, 0, 0, 0, DateTimeKind.Local) },
                    { 2, 6.5, 60, 77.700000000000003, "Denmark", 65.200000000000003, 65.0, new DateTime(2020, 4, 27, 0, 0, 0, 0, DateTimeKind.Local) },
                    { 3, 2.7999999999999998, 20, 68.400000000000006, "Sweden", 90.200000000000003, 30.0, new DateTime(2020, 4, 27, 0, 0, 0, 0, DateTimeKind.Local) },
                    { 4, 8.6999999999999993, 3, 16.699999999999999, "Belgium", 90.200000000000003, 30.0, new DateTime(2020, 4, 27, 0, 0, 0, 0, DateTimeKind.Local) },
                    { 5, 90.799999999999997, 0, 20.699999999999999, "California", 43.200000000000003, 6.0, new DateTime(2020, 4, 27, 0, 0, 0, 0, DateTimeKind.Local) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "WeatherObservations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "WeatherObservations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "WeatherObservations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "WeatherObservations",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "WeatherObservations",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
