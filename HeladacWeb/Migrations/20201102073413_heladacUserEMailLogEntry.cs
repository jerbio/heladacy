using Microsoft.EntityFrameworkCore.Migrations;

namespace HeladacWeb.Migrations
{
    public partial class heladacUserEMailLogEntry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "heladacUserId",
                table: "EmailLogEntrys",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_EmailLogEntrys_heladacUserId",
                table: "EmailLogEntrys",
                column: "heladacUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailLogEntrys_AspNetUsers_heladacUserId",
                table: "EmailLogEntrys",
                column: "heladacUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailLogEntrys_AspNetUsers_heladacUserId",
                table: "EmailLogEntrys");

            migrationBuilder.DropIndex(
                name: "IX_EmailLogEntrys_heladacUserId",
                table: "EmailLogEntrys");

            migrationBuilder.DropColumn(
                name: "heladacUserId",
                table: "EmailLogEntrys");
        }
    }
}
