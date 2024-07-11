using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShopDQ.App.Migrations
{
    public partial class FixChats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.AddColumn<string>(
                name: "Messages",
                table: "Chats",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4b8c4db7-326e-40e2-8707-92885543c516"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4cbf74be-b599-45bb-9beb-7cd74608b6a1"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("d29816f6-7f5b-4f71-86b8-fd5900c71859"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("e0667d8b-8276-4721-93d3-465905f433a6"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("fb5a838e-db29-40a7-ba51-59d506a07733"));

            migrationBuilder.DropColumn(
                name: "Messages",
                table: "Chats");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("3d5b2ca9-fb54-4b4d-bf91-383d1460c19b"), "5", "Role", "User", "USER" },
                    { new Guid("760bf101-8299-47c2-878c-4835c638b897"), "4", "Role", "Seller", "SELLER" },
                    { new Guid("ab713fdd-8451-44b9-9eab-128d7e7dba0a"), "1", "Role", "Admin", "ADMIN" },
                    { new Guid("acf390a4-6cd3-4b01-81da-ff9726678a57"), "3", "Role", "Shiper", "SHIPER" },
                    { new Guid("b15ec1a3-342f-4e9e-bb36-b0cba16ad513"), "2", "Role", "Manager", "MANAGER" }
                });
        }
    }
}
