using Microsoft.EntityFrameworkCore.Migrations;

namespace HeladacWeb.Migrations
{
    public partial class sentMailCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SentMails",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    heladacUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SentMails", x => x.id);
                    table.ForeignKey(
                        name: "FK_SentMails_Emails_id",
                        column: x => x.id,
                        principalTable: "Emails",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SentMails_HelmUsers_heladacUserId",
                        column: x => x.heladacUserId,
                        principalTable: "HelmUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SentMails_heladacUserId",
                table: "SentMails",
                column: "heladacUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropTable(
                name: "SentMails");
        }
    }
}
