using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShopDQ.App.Migrations
{
    public partial class addCategory_V3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AddColumn<string>(
                name: "CategoryPath",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);

            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("607eaa40-662c-4046-933d-4332b67b652f"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("87d69548-9c02-4516-8291-8b5edc00adea"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("bbc50b02-7121-4bfe-8212-f420bf7deffc"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("efbb88dd-9d8e-4642-9a53-50c2f6db508e"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("f2f2d177-a565-468f-a93f-e49715842e51"));

            migrationBuilder.DropColumn(
                name: "CategoryPath",
                table: "Categories");

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
    }
}
