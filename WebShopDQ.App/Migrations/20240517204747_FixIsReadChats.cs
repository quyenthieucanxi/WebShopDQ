using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShopDQ.App.Migrations
{
    public partial class FixIsReadChats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.RenameColumn(
                name: "isRead",
                table: "Chats",
                newName: "isSenderRead");

            migrationBuilder.AddColumn<bool>(
                name: "isReceiverRead",
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
                keyValue: new Guid("33037256-5b4b-4f69-82db-0a0f659f7a03"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("57b4b322-3dab-4cab-bb43-337ea32709fe"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7d65c211-f9a0-42f8-afb1-ec4aaa6cfa5d"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c213127c-c4ed-48a9-adbf-f4fddd662584"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("db6c26ec-05ad-4d8c-b270-88119f93f900"));

            migrationBuilder.DropColumn(
                name: "isReceiverRead",
                table: "Chats");

            migrationBuilder.RenameColumn(
                name: "isSenderRead",
                table: "Chats",
                newName: "isRead");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("1e086d5f-bb84-436f-9ed6-547d3d1139ff"), "1", "Role", "Admin", "ADMIN" },
                    { new Guid("2d999390-e3aa-4761-b8da-eeac43414e44"), "3", "Role", "Shiper", "SHIPER" },
                    { new Guid("4fd9390c-ba18-470e-9573-986e4e514547"), "5", "Role", "User", "USER" },
                    { new Guid("50fb9c2a-b8da-43fa-9123-5a49cdb0b2dc"), "4", "Role", "Seller", "SELLER" },
                    { new Guid("dcad8a10-d19e-40ca-a1f5-2ab937826d25"), "2", "Role", "Manager", "MANAGER" }
                });
        }
    }
}
