using Microsoft.EntityFrameworkCore.Migrations;

namespace HeladacWeb.Migrations
{
    public partial class creditcard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "countryCode",
                table: "PhoneNumbers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "extension",
                table: "PhoneNumbers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "phoneNumber",
                table: "PhoneNumbers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "creditCardId",
                table: "HelmUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CreditCard",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    firstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    address1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    address2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    misc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    city = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    state = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    postal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ccNumber = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    cvvCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    expiryMonth = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    expiryYear = table.Column<int>(type: "int", nullable: false),
                    heladacUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCard", x => x.id);
                    table.ForeignKey(
                        name: "FK_CreditCard_AspNetUsers_heladacUserId",
                        column: x => x.heladacUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HelmUsers_creditCardId",
                table: "HelmUsers",
                column: "creditCardId");

            migrationBuilder.CreateIndex(
                name: "HeladacUser_CreditCardNumber",
                table: "CreditCard",
                columns: new[] { "heladacUserId", "ccNumber" },
                unique: true,
                filter: "[ccNumber] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_HelmUsers_CreditCard_creditCardId",
                table: "HelmUsers",
                column: "creditCardId",
                principalTable: "CreditCard",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HelmUsers_CreditCard_creditCardId",
                table: "HelmUsers");

            migrationBuilder.DropTable(
                name: "CreditCard");

            migrationBuilder.DropIndex(
                name: "IX_HelmUsers_creditCardId",
                table: "HelmUsers");

            migrationBuilder.DropColumn(
                name: "countryCode",
                table: "PhoneNumbers");

            migrationBuilder.DropColumn(
                name: "extension",
                table: "PhoneNumbers");

            migrationBuilder.DropColumn(
                name: "phoneNumber",
                table: "PhoneNumbers");

            migrationBuilder.DropColumn(
                name: "creditCardId",
                table: "HelmUsers");
        }
    }
}
