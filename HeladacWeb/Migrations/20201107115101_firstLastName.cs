using Microsoft.EntityFrameworkCore.Migrations;

namespace HeladacWeb.Migrations
{
    public partial class firstLastName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "firstName",
                table: "HelmUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "lastName",
                table: "HelmUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "middleName",
                table: "HelmUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "firstName",
                table: "HelmUsers");

            migrationBuilder.DropColumn(
                name: "lastName",
                table: "HelmUsers");

            migrationBuilder.DropColumn(
                name: "middleName",
                table: "HelmUsers");
        }
    }
}
