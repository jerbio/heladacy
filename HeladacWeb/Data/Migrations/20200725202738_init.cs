using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HeladacWeb.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.CreateTable(
                name: "Emails",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    subJect_DB = table.Column<string>(nullable: true),
                    timeOfCreationMs = table.Column<long>(nullable: false),
                    sender_DB = table.Column<string>(nullable: true),
                    bcc_DB = table.Column<string>(nullable: true),
                    cc_DB = table.Column<string>(nullable: true),
                    content_DB = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    heladacUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emails", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "HelmUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    heladacUserId = table.Column<string>(nullable: false),
                    address1_DB = table.Column<string>(nullable: true),
                    address2_DB = table.Column<string>(nullable: true),
                    city_DB = table.Column<string>(nullable: true),
                    state_DB = table.Column<string>(nullable: true),
                    country_DB = table.Column<string>(nullable: true),
                    postal_DB = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HelmUsers", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_HelmUsers_AspNetUsers_heladacUserId",
                        column: x => x.heladacUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Credentials",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    helmUserId = table.Column<string>(nullable: true),
                    credentialService_DB = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Credentials", x => x.id);
                    table.ForeignKey(
                        name: "FK_Credentials_HelmUsers_helmUserId",
                        column: x => x.helmUserId,
                        principalTable: "HelmUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmailLogEntrys",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    creationTime_DB = table.Column<string>(nullable: true),
                    userId = table.Column<string>(nullable: false),
                    emailId = table.Column<string>(nullable: false),
                    isDeleted_DB = table.Column<bool>(nullable: false),
                    timeOfDeletionMs_DB = table.Column<long>(nullable: false),
                    isArchived_DB = table.Column<bool>(nullable: false),
                    timeOfLastArchiveMs_DB = table.Column<long>(nullable: false),
                    isRead_DB = table.Column<bool>(nullable: false),
                    readToggleHistory_DB = table.Column<string>(nullable: true),
                    archiveToggleHistory_DB = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailLogEntrys", x => x.id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_EmailLogEntrys_Emails_emailId",
                        column: x => x.emailId,
                        principalTable: "Emails",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmailLogEntrys_HelmUsers_userId",
                        column: x => x.userId,
                        principalTable: "HelmUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Credentials_helmUserId",
                table: "Credentials",
                column: "helmUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailLogEntrys_emailId",
                table: "EmailLogEntrys",
                column: "emailId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailLogEntrys_userId_emailId",
                table: "EmailLogEntrys",
                columns: new[] { "userId", "emailId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UserId_Email",
                table: "EmailLogEntrys",
                columns: new[] { "userId", "id" },
                unique: true)
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.CreateIndex(
                name: "UserId_CreationTime_Email",
                table: "EmailLogEntrys",
                columns: new[] { "userId", "creationTime_DB", "id" },
                unique: true,
                filter: "[creationTime_DB] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "HelmUser_HeladacUser",
                table: "HelmUsers",
                columns: new[] { "heladacUserId", "Id" },
                unique: true)
                .Annotation("SqlServer:Clustered", true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Credentials");

            migrationBuilder.DropTable(
                name: "EmailLogEntrys");

            migrationBuilder.DropTable(
                name: "Emails");

            migrationBuilder.DropTable(
                name: "HelmUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
