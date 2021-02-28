using Microsoft.EntityFrameworkCore.Migrations;

namespace HeladacWeb.Migrations
{
    public partial class expiration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "expirationTimeMs_DB",
                table: "HelmUsers",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "expirationTimeMs_DB",
                table: "HelmUsers");
        }
    }
}
