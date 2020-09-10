using Microsoft.EntityFrameworkCore.Migrations;

namespace HeladacWeb.Data.Migrations
{
    public partial class emailReceiver : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "creationTimeMs_DB",
                table: "HelmUsers",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "HelmUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "HelmUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "receiver_DB",
                table: "Emails",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "creationTimeMs_DB",
                table: "HelmUsers");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "HelmUsers");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "HelmUsers");

            migrationBuilder.DropColumn(
                name: "receiver_DB",
                table: "Emails");
        }
    }
}
