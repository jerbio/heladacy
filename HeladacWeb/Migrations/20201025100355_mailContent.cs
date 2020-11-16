using Microsoft.EntityFrameworkCore.Migrations;

namespace HeladacWeb.Migrations
{
    public partial class mailContent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "content_DB",
                table: "Emails");

            migrationBuilder.DropColumn(
                name: "content_html_DB",
                table: "Emails");

            migrationBuilder.AddColumn<string>(
                name: "mailContentId",
                table: "Emails",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MailContent",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    content_DB = table.Column<string>(nullable: true),
                    content_html_DB = table.Column<string>(nullable: true),
                    raw = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailContent", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Emails_mailContentId",
                table: "Emails",
                column: "mailContentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Emails_MailContent_mailContentId",
                table: "Emails",
                column: "mailContentId",
                principalTable: "MailContent",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Emails_MailContent_mailContentId",
                table: "Emails");

            migrationBuilder.DropTable(
                name: "MailContent");

            migrationBuilder.DropIndex(
                name: "IX_Emails_mailContentId",
                table: "Emails");

            migrationBuilder.DropColumn(
                name: "mailContentId",
                table: "Emails");

            migrationBuilder.AddColumn<string>(
                name: "content_DB",
                table: "Emails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "content_html_DB",
                table: "Emails",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
