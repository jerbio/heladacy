using Microsoft.EntityFrameworkCore.Migrations;

namespace HeladacWeb.Migrations
{
    public partial class logEntryDeletion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "HelmUsers");

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted_DB",
                table: "HelmUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isDeleted_DB",
                table: "HelmUsers");

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "HelmUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
