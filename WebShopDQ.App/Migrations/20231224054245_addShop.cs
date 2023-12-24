using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShopDQ.App.Migrations
{
    public partial class addShop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
 
            migrationBuilder.CreateTable(
                name: "Shops",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Path = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shops", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shops_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AddressShippings_AddressShippingID",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Posts_ProductID",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Categories_CategoryID",
                table: "Posts");

            migrationBuilder.DropTable(
                name: "AddressShippings");

            migrationBuilder.DropTable(
                name: "Shops");

            migrationBuilder.DropIndex(
                name: "IX_Orders_AddressShippingID",
                table: "Orders");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("809fa679-63d3-481b-9792-39507dbbb494"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("99bb2def-5ed9-438e-a171-018045c9ae30"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("b4a3cb5c-da9c-4186-81b7-28eb410972c2"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("d44679ec-514d-4f5f-aea1-7748cd6ed8ce"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("da13220a-3ac6-4276-a919-2e69ae2820c4"));

            migrationBuilder.DropColumn(
                name: "PostPath",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "AddressShippingID",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "Payment",
                table: "Orders",
                newName: "PhoneNumber");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Posts",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1500)",
                oldMaxLength: 1500);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "CategoryPath",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("0537e971-dcb1-476e-8eeb-f2d5d6923332"), "3", "Role", "Shiper", "SHIPER" },
                    { new Guid("241df9fa-6f5a-483a-9876-418794b6ab42"), "2", "Role", "Manager", "MANAGER" },
                    { new Guid("860cc4f3-ae71-450e-af02-70c9262222d7"), "5", "Role", "User", "USER" },
                    { new Guid("94234755-86b8-4cab-890a-e2b63f98162e"), "4", "Role", "Seller", "SELLER" },
                    { new Guid("d5db9ce0-fd8f-45cc-b4e3-27b3cd5f23bf"), "1", "Role", "Admin", "ADMIN" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Posts_ProductID",
                table: "Orders",
                column: "ProductID",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Categories_CategoryID",
                table: "Posts",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
