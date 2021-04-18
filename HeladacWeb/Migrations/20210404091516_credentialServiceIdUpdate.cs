using Microsoft.EntityFrameworkCore.Migrations;

namespace HeladacWeb.Migrations
{
    public partial class credentialServiceIdUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HelmUsers_CredentialServices_credentialService_DBid",
                table: "HelmUsers");

            migrationBuilder.RenameColumn(
                name: "credentialService_DBid",
                table: "HelmUsers",
                newName: "credentialServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_HelmUsers_credentialService_DBid",
                table: "HelmUsers",
                newName: "IX_HelmUsers_credentialServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_HelmUsers_CredentialServices_credentialServiceId",
                table: "HelmUsers",
                column: "credentialServiceId",
                principalTable: "CredentialServices",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HelmUsers_CredentialServices_credentialServiceId",
                table: "HelmUsers");

            migrationBuilder.RenameColumn(
                name: "credentialServiceId",
                table: "HelmUsers",
                newName: "credentialService_DBid");

            migrationBuilder.RenameIndex(
                name: "IX_HelmUsers_credentialServiceId",
                table: "HelmUsers",
                newName: "IX_HelmUsers_credentialService_DBid");

            migrationBuilder.AddForeignKey(
                name: "FK_HelmUsers_CredentialServices_credentialService_DBid",
                table: "HelmUsers",
                column: "credentialService_DBid",
                principalTable: "CredentialServices",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
