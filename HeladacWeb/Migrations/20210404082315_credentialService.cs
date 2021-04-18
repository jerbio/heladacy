using Microsoft.EntityFrameworkCore.Migrations;

namespace HeladacWeb.Migrations
{
    public partial class credentialService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "credentialService_DBid",
                table: "HelmUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CredentialServices",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ServiceType_DB = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Domain_DB = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Url_DB = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CredentialServices", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HelmUsers_credentialService_DBid",
                table: "HelmUsers",
                column: "credentialService_DBid");

            migrationBuilder.CreateIndex(
                name: "CredentialService_Domain",
                table: "CredentialServices",
                columns: new[] { "ServiceType_DB", "Domain_DB" },
                unique: true,
                filter: "[ServiceType_DB] IS NOT NULL AND [Domain_DB] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_HelmUsers_CredentialServices_credentialService_DBid",
                table: "HelmUsers",
                column: "credentialService_DBid",
                principalTable: "CredentialServices",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HelmUsers_CredentialServices_credentialService_DBid",
                table: "HelmUsers");

            migrationBuilder.DropTable(
                name: "CredentialServices");

            migrationBuilder.DropIndex(
                name: "IX_HelmUsers_credentialService_DBid",
                table: "HelmUsers");

            migrationBuilder.DropColumn(
                name: "credentialService_DBid",
                table: "HelmUsers");
        }
    }
}
