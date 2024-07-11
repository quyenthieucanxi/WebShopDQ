using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShopDQ.App.Migrations
{
    public partial class AddisReadForChat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isRead",
                table: "Chats",
                type: "bit",
                nullable: false,
                defaultValue: false);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("1e086d5f-bb84-436f-9ed6-547d3d1139ff"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("2d999390-e3aa-4761-b8da-eeac43414e44"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4fd9390c-ba18-470e-9573-986e4e514547"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("50fb9c2a-b8da-43fa-9123-5a49cdb0b2dc"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("dcad8a10-d19e-40ca-a1f5-2ab937826d25"));

            migrationBuilder.DropColumn(
                name: "isRead",
                table: "Chats");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("4b8c4db7-326e-40e2-8707-92885543c516"), "3", "Role", "Shiper", "SHIPER" },
                    { new Guid("4cbf74be-b599-45bb-9beb-7cd74608b6a1"), "2", "Role", "Manager", "MANAGER" },
                    { new Guid("d29816f6-7f5b-4f71-86b8-fd5900c71859"), "5", "Role", "User", "USER" },
                    { new Guid("e0667d8b-8276-4721-93d3-465905f433a6"), "4", "Role", "Seller", "SELLER" },
                    { new Guid("fb5a838e-db29-40a7-ba51-59d506a07733"), "1", "Role", "Admin", "ADMIN" }
                });
        }
    }
}
