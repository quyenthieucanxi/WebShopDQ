using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShopDQ.App.Migrations
{
    public partial class addCategory_V2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("45d0c82c-8246-4db7-9ce0-c19dfb48549b"), "1", "Role", "Admin", "ADMIN" },
                    { new Guid("4bdf6633-5b47-4134-8617-ddfe02fa655e"), "5", "Role", "User", "USER" },
                    { new Guid("5a7db289-eaf0-4d2a-b3c5-0be1ccedcea1"), "3", "Role", "Shiper", "SHIPER" },
                    { new Guid("5e730ffa-87c1-4895-9eb1-538664ad4b30"), "4", "Role", "Seller", "SELLER" },
                    { new Guid("afc91fce-84be-40d5-bb57-81e46e20cda7"), "2", "Role", "Manager", "MANAGER" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("45d0c82c-8246-4db7-9ce0-c19dfb48549b"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4bdf6633-5b47-4134-8617-ddfe02fa655e"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("5a7db289-eaf0-4d2a-b3c5-0be1ccedcea1"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("5e730ffa-87c1-4895-9eb1-538664ad4b30"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("afc91fce-84be-40d5-bb57-81e46e20cda7"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("036d36df-1d5b-42ef-83c7-2857795d7820"), "2", "Role", "Manager", "MANAGER" },
                    { new Guid("50c10619-d623-4417-95fc-04bf20ce7acf"), "4", "Role", "Seller", "SELLER" },
                    { new Guid("53f28353-e12d-4528-9095-35ebd749a68c"), "3", "Role", "Shiper", "SHIPER" },
                    { new Guid("69f79838-add5-46eb-a44a-9c5859fbb1e9"), "5", "Role", "User", "USER" },
                    { new Guid("7b63ef13-5041-4c32-8f63-9da210bfa6aa"), "1", "Role", "Admin", "ADMIN" }
                });
        }
    }
}
