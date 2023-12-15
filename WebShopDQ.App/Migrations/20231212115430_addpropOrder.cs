using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShopDQ.App.Migrations
{
    public partial class addpropOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AddColumn<string>(
                name: "Payment",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "CategoryPath",
                table: "Categories",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

          
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("109c283b-c20e-4761-9a6c-485481857849"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("5be31c67-0581-4c2b-ba55-c3328f686f11"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("a287a254-4668-4b45-a666-5323aa6159c7"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("cab63a99-b90d-4e9c-9630-a24b2e9c6b41"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("ef7bb1ca-d27e-47c5-9a80-b9eedcf360c6"));

            migrationBuilder.DropColumn(
                name: "Payment",
                table: "Orders");

            migrationBuilder.AlterColumn<string>(
                name: "CategoryPath",
                table: "Categories",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("2d978559-c1ab-4ee1-b439-09362574f173"), "3", "Role", "Shiper", "SHIPER" },
                    { new Guid("7b893e62-9ffb-4a0c-bcdf-91b15dc0286a"), "1", "Role", "Admin", "ADMIN" },
                    { new Guid("8155cc34-0344-4189-a955-1a394ff3fe7f"), "4", "Role", "Seller", "SELLER" },
                    { new Guid("951b1404-b6ea-40fe-8d0d-c1693b7cfcf8"), "5", "Role", "User", "USER" },
                    { new Guid("b5a62cde-4269-4603-856d-725e334d966a"), "2", "Role", "Manager", "MANAGER" }
                });
        }
    }
}
