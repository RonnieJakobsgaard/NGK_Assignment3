using Microsoft.EntityFrameworkCore.Migrations;

namespace WeatherStation.Web.Api.Migrations
{
    public partial class NameEdit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Measurements_WeatherStations_WeatherStationName",
                table: "Measurements");

            migrationBuilder.RenameColumn(
                name: "WeatherStationName",
                table: "Measurements",
                newName: "LocalWeatherStationName");

            migrationBuilder.RenameIndex(
                name: "IX_Measurements_WeatherStationName",
                table: "Measurements",
                newName: "IX_Measurements_LocalWeatherStationName");

            migrationBuilder.AddForeignKey(
                name: "FK_Measurements_WeatherStations_LocalWeatherStationName",
                table: "Measurements",
                column: "LocalWeatherStationName",
                principalTable: "WeatherStations",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Measurements_WeatherStations_LocalWeatherStationName",
                table: "Measurements");

            migrationBuilder.RenameColumn(
                name: "LocalWeatherStationName",
                table: "Measurements",
                newName: "WeatherStationName");

            migrationBuilder.RenameIndex(
                name: "IX_Measurements_LocalWeatherStationName",
                table: "Measurements",
                newName: "IX_Measurements_WeatherStationName");

            migrationBuilder.AddForeignKey(
                name: "FK_Measurements_WeatherStations_WeatherStationName",
                table: "Measurements",
                column: "WeatherStationName",
                principalTable: "WeatherStations",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
