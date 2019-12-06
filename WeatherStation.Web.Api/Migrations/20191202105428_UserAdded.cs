using Microsoft.EntityFrameworkCore.Migrations;

namespace WeatherStation.Web.Api.Migrations
{
    public partial class UserAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "WeatherStations",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    UserName = table.Column<string>(nullable: false),
                    Password = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.UserName);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WeatherStations_UserName",
                table: "WeatherStations",
                column: "UserName");

            migrationBuilder.AddForeignKey(
                name: "FK_WeatherStations_users_UserName",
                table: "WeatherStations",
                column: "UserName",
                principalTable: "users",
                principalColumn: "UserName",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WeatherStations_users_UserName",
                table: "WeatherStations");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropIndex(
                name: "IX_WeatherStations_UserName",
                table: "WeatherStations");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "WeatherStations");
        }
    }
}
