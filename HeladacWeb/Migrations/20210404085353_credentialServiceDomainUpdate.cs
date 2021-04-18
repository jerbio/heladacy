using Microsoft.EntityFrameworkCore.Migrations;

namespace HeladacWeb.Migrations
{
    public partial class credentialServiceDomainUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "CredentialService_Domain",
                table: "CredentialServices");

            migrationBuilder.AlterColumn<string>(
                name: "ServiceType_DB",
                table: "CredentialServices",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "CredentialService_Domain",
                table: "CredentialServices",
                column: "Domain_DB",
                unique: true,
                filter: "[Domain_DB] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "CredentialService_Domain",
                table: "CredentialServices");

            migrationBuilder.AlterColumn<string>(
                name: "ServiceType_DB",
                table: "CredentialServices",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "CredentialService_Domain",
                table: "CredentialServices",
                columns: new[] { "ServiceType_DB", "Domain_DB" },
                unique: true,
                filter: "[ServiceType_DB] IS NOT NULL AND [Domain_DB] IS NOT NULL");
        }
    }
}
