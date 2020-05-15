using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NGKHandIn3.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    Token = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "WeatherObservations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Time = table.Column<DateTime>(nullable: false),
                    LocationName = table.Column<string>(nullable: true),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    Temperature = table.Column<double>(nullable: false),
                    HumidityPercentage = table.Column<int>(nullable: false),
                    AirPressure = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherObservations", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "WeatherObservations",
                columns: new[] { "Id", "AirPressure", "HumidityPercentage", "Latitude", "LocationName", "Longitude", "Temperature", "Time" },
                values: new object[,]
                {
                    { 1, 2.7999999999999998, 20, 66.700000000000003, "Florida", 90.200000000000003, 30.0, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 6.5, 60, 77.700000000000003, "Denmark", 65.200000000000003, 65.0, new DateTime(2020, 5, 4, 0, 0, 0, 0, DateTimeKind.Local) },
                    { 3, 2.7999999999999998, 20, 68.400000000000006, "Sweden", 90.200000000000003, 30.0, new DateTime(2020, 5, 14, 3, 0, 0, 0, DateTimeKind.Local) },
                    { 4, 8.6999999999999993, 3, 16.699999999999999, "Belgium", 90.200000000000003, 30.0, new DateTime(2020, 5, 14, 4, 0, 0, 0, DateTimeKind.Local) },
                    { 5, 90.799999999999997, 0, 20.699999999999999, "California", 43.200000000000003, 6.0, new DateTime(2020, 5, 14, 5, 0, 0, 0, DateTimeKind.Local) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "WeatherObservations");
        }
    }
}
