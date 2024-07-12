using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShopDQ.App.Migrations
{
    public partial class FixconfigCate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("3d0f4ebe-a247-4681-864e-3de3ae5940c6"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("3dfb2bc9-5441-46b9-8d97-5c435fb9c3fc"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("76b452dc-c631-490d-ab2d-f39202df1cb6"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("87561672-750b-4f03-a28f-b7869b5ecfd5"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("a0a4e032-708c-4458-b01a-8840e5b535b6"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("44f1e467-16d7-416a-8880-6f03b51fe044"), "3", "Role", "Shiper", "SHIPER" },
                    { new Guid("5c020fcc-223f-46ca-b2d9-26359a346db1"), "5", "Role", "User", "USER" },
                    { new Guid("b419c547-c536-41a9-948d-b74bc9cac026"), "1", "Role", "Admin", "ADMIN" },
                    { new Guid("b72a30de-5e6d-4b5c-acf4-a289c3be7113"), "2", "Role", "Manager", "MANAGER" },
                    { new Guid("eda95256-7003-4cbc-934a-5b27d0aadc2d"), "4", "Role", "Seller", "SELLER" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("44f1e467-16d7-416a-8880-6f03b51fe044"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("5c020fcc-223f-46ca-b2d9-26359a346db1"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("b419c547-c536-41a9-948d-b74bc9cac026"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("b72a30de-5e6d-4b5c-acf4-a289c3be7113"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("eda95256-7003-4cbc-934a-5b27d0aadc2d"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("3d0f4ebe-a247-4681-864e-3de3ae5940c6"), "2", "Role", "Manager", "MANAGER" },
                    { new Guid("3dfb2bc9-5441-46b9-8d97-5c435fb9c3fc"), "1", "Role", "Admin", "ADMIN" },
                    { new Guid("76b452dc-c631-490d-ab2d-f39202df1cb6"), "4", "Role", "Seller", "SELLER" },
                    { new Guid("87561672-750b-4f03-a28f-b7869b5ecfd5"), "5", "Role", "User", "USER" },
                    { new Guid("a0a4e032-708c-4458-b01a-8840e5b535b6"), "3", "Role", "Shiper", "SHIPER" }
                });
        }
    }
}
