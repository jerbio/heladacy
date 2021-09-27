using Microsoft.EntityFrameworkCore.Migrations;

namespace HeladacWeb.Migrations
{
    public partial class phoneNumberImplementation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "phoneNumber",
                table: "HelmUsers");

            migrationBuilder.AddColumn<string>(
                name: "phoneNumberId",
                table: "HelmUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "latestPhoneNumberId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PhoneNumberList",
                columns: table => new
                {
                    heladacUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneNumberList", x => x.heladacUserId);
                    table.ForeignKey(
                        name: "FK_PhoneNumberList_AspNetUsers_heladacUserId",
                        column: x => x.heladacUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhoneNumbers",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    creationTime = table.Column<long>(type: "bigint", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    isGeneral = table.Column<bool>(type: "bit", nullable: false),
                    isVerifiedActive = table.Column<bool>(type: "bit", nullable: false),
                    fullNumber = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PhoneNumberListheladacUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneNumbers", x => x.id);
                    table.ForeignKey(
                        name: "FK_PhoneNumbers_PhoneNumberList_PhoneNumberListheladacUserId",
                        column: x => x.PhoneNumberListheladacUserId,
                        principalTable: "PhoneNumberList",
                        principalColumn: "heladacUserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HelmUsers_phoneNumberId",
                table: "HelmUsers",
                column: "phoneNumberId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_latestPhoneNumberId",
                table: "AspNetUsers",
                column: "latestPhoneNumberId");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneNumbers_PhoneNumberListheladacUserId",
                table: "PhoneNumbers",
                column: "PhoneNumberListheladacUserId");

            migrationBuilder.CreateIndex(
                name: "PhoneNumber",
                table: "PhoneNumbers",
                column: "fullNumber",
                unique: true,
                filter: "[fullNumber] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_PhoneNumbers_latestPhoneNumberId",
                table: "AspNetUsers",
                column: "latestPhoneNumberId",
                principalTable: "PhoneNumbers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HelmUsers_PhoneNumbers_phoneNumberId",
                table: "HelmUsers",
                column: "phoneNumberId",
                principalTable: "PhoneNumbers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_PhoneNumbers_latestPhoneNumberId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_HelmUsers_PhoneNumbers_phoneNumberId",
                table: "HelmUsers");

            migrationBuilder.DropTable(
                name: "PhoneNumbers");

            migrationBuilder.DropTable(
                name: "PhoneNumberList");

            migrationBuilder.DropIndex(
                name: "IX_HelmUsers_phoneNumberId",
                table: "HelmUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_latestPhoneNumberId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "phoneNumberId",
                table: "HelmUsers");

            migrationBuilder.DropColumn(
                name: "latestPhoneNumberId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "phoneNumber",
                table: "HelmUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
