using Microsoft.EntityFrameworkCore.Migrations;

namespace HeladacWeb.Data.Migrations
{
    public partial class emailLogEntry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UserId_CreationTime_Email",
                table: "EmailLogEntrys");

            migrationBuilder.AlterColumn<long>(
                name: "creationTime_DB",
                table: "EmailLogEntrys",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "senderEmail_DB",
                table: "EmailLogEntrys",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "UserId_CreationTime_Email",
                table: "EmailLogEntrys",
                columns: new[] { "userId", "creationTime_DB", "id" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UserId_CreationTime_Email",
                table: "EmailLogEntrys");

            migrationBuilder.DropColumn(
                name: "senderEmail_DB",
                table: "EmailLogEntrys");

            migrationBuilder.AlterColumn<string>(
                name: "creationTime_DB",
                table: "EmailLogEntrys",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.CreateIndex(
                name: "UserId_CreationTime_Email",
                table: "EmailLogEntrys",
                columns: new[] { "userId", "creationTime_DB", "id" },
                unique: true,
                filter: "[creationTime_DB] IS NOT NULL");
        }
    }
}
