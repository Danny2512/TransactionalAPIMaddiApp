using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TransactionalAPIMaddiApp.Migrations
{
    public partial class InitialDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblAssetsImage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StrImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAssetsImage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StrName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StrUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StrDocument = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StrEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BiEmailConfirm = table.Column<bool>(type: "bit", nullable: false),
                    StrPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BiPhoneConfirm = table.Column<bool>(type: "bit", nullable: false),
                    HsPassword = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    BiActive = table.Column<bool>(type: "bit", nullable: false),
                    StrOTP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DtExpirationDateOTP = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StrRemark = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DtLastLogin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DtLastPasswordChange = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HsLastPassword = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    IntLoginFailed = table.Column<int>(type: "int", nullable: false),
                    DtUnlookDt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DtLastFailedLogin = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblRestaurant",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StrName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StrNit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Logo_AssetsFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StrDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StrWebsite = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IntCantSedes = table.Column<int>(type: "int", nullable: false),
                    BiActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblRestaurant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblRestaurant_tblAssetsImage_Logo_AssetsFK",
                        column: x => x.Logo_AssetsFK,
                        principalTable: "tblAssetsImage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblRestaurant_tblUser_UserFK",
                        column: x => x.UserFK,
                        principalTable: "tblUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblHeadquarter",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RestaurantFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StrName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StrAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DtStart = table.Column<TimeSpan>(type: "time", nullable: false),
                    DtEnd = table.Column<TimeSpan>(type: "time", nullable: false),
                    BiBooking = table.Column<bool>(type: "bit", nullable: false),
                    BiOrderTable = table.Column<bool>(type: "bit", nullable: false),
                    BiDelivery = table.Column<bool>(type: "bit", nullable: false),
                    BiActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblHeadquarter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblHeadquarter_tblRestaurant_RestaurantFK",
                        column: x => x.RestaurantFK,
                        principalTable: "tblRestaurant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblHeadquarter_RestaurantFK",
                table: "tblHeadquarter",
                column: "RestaurantFK");

            migrationBuilder.CreateIndex(
                name: "IX_tblRestaurant_Logo_AssetsFK",
                table: "tblRestaurant",
                column: "Logo_AssetsFK",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblRestaurant_UserFK",
                table: "tblRestaurant",
                column: "UserFK",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblHeadquarter");

            migrationBuilder.DropTable(
                name: "tblRestaurant");

            migrationBuilder.DropTable(
                name: "tblAssetsImage");

            migrationBuilder.DropTable(
                name: "tblUser");
        }
    }
}
