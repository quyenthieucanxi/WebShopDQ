using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShopDQ.App.Migrations
{
    public partial class addCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<string>(
                name: "urlImg",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("036d36df-1d5b-42ef-83c7-2857795d7820"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("50c10619-d623-4417-95fc-04bf20ce7acf"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("53f28353-e12d-4528-9095-35ebd749a68c"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("69f79838-add5-46eb-a44a-9c5859fbb1e9"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7b63ef13-5041-4c32-8f63-9da210bfa6aa"));

            migrationBuilder.DropColumn(
                name: "urlImg",
                table: "Categories");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("03e769a3-a3fa-49b2-9594-7c4f548ce533"), "4", "Role", "Seller", "SELLER" },
                    { new Guid("88df8b94-df09-4154-84d5-7c934900e224"), "3", "Role", "Shiper", "SHIPER" },
                    { new Guid("97ecef3e-f964-4b65-9173-2274f0863352"), "2", "Role", "Manager", "MANAGER" },
                    { new Guid("d38c60aa-ab43-438a-b739-0cbae99b6eb7"), "5", "Role", "User", "USER" },
                    { new Guid("e53219ef-b101-41dd-b593-f28ff12901cd"), "1", "Role", "Admin", "ADMIN" }
                });
        }
    }
}
