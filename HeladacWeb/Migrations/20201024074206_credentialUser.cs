using Microsoft.EntityFrameworkCore.Migrations;

namespace HeladacWeb.Migrations
{
    public partial class credentialUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "password",
                table: "HelmUsers");

            migrationBuilder.AddColumn<string>(
                name: "passwordHash",
                table: "HelmUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "phoneNumber",
                table: "HelmUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "credentialId",
                table: "Credentials",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "heladacUserId",
                table: "Credentials",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Credentials_credentialId",
                table: "Credentials",
                column: "credentialId");

            migrationBuilder.CreateIndex(
                name: "IX_Credentials_heladacUserId",
                table: "Credentials",
                column: "heladacUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Credentials_HelmUsers_credentialId",
                table: "Credentials",
                column: "credentialId",
                principalTable: "HelmUsers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Credentials_HelmUsers_heladacUserId",
                table: "Credentials",
                column: "heladacUserId",
                principalTable: "HelmUsers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Credentials_HelmUsers_credentialId",
                table: "Credentials");

            migrationBuilder.DropForeignKey(
                name: "FK_Credentials_HelmUsers_heladacUserId",
                table: "Credentials");

            migrationBuilder.DropIndex(
                name: "IX_Credentials_credentialId",
                table: "Credentials");

            migrationBuilder.DropIndex(
                name: "IX_Credentials_heladacUserId",
                table: "Credentials");

            migrationBuilder.DropColumn(
                name: "passwordHash",
                table: "HelmUsers");

            migrationBuilder.DropColumn(
                name: "phoneNumber",
                table: "HelmUsers");

            migrationBuilder.DropColumn(
                name: "credentialId",
                table: "Credentials");

            migrationBuilder.DropColumn(
                name: "heladacUserId",
                table: "Credentials");

            migrationBuilder.AddColumn<string>(
                name: "password",
                table: "HelmUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
