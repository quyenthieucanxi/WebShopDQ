using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShopDQ.App.Migrations
{
    public partial class FixmodelShop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Shops",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("30d28771-22f5-4074-9cc5-b99dffadad12"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("5bdd7573-9b90-4d5b-b5da-579a217e4fd3"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("6d61a132-66b7-4dea-890a-aafe03877693"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("a10753b5-8214-4082-b422-ac48834c9385"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("aeb0e8e1-5f10-4b83-9811-a3db6ea7a1f4"));

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Shops");

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
