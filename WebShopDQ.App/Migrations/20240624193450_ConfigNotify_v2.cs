using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShopDQ.App.Migrations
{
    public partial class ConfigNotify_v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifies_Users_UserIdReceiver",
                table: "Notifies");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifies_Users_UserIdSender",
                table: "Notifies");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifies_Users_UserIdReceiver",
                table: "Notifies",
                column: "UserIdReceiver",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifies_Users_UserIdSender",
                table: "Notifies",
                column: "UserIdSender",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifies_Users_UserIdReceiver",
                table: "Notifies");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifies_Users_UserIdSender",
                table: "Notifies");

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
                    { new Guid("3c0a0abe-1e17-45f4-b4d7-a33e8b005774"), "4", "Role", "Seller", "SELLER" },
                    { new Guid("4c8bf438-04e7-43c2-b0b8-8c234b9f5356"), "3", "Role", "Shiper", "SHIPER" },
                    { new Guid("74d52502-0889-4a23-9ac7-51acae9e62f1"), "1", "Role", "Admin", "ADMIN" },
                    { new Guid("836fc6f2-20ee-431e-90b5-144fddb03d99"), "2", "Role", "Manager", "MANAGER" },
                    { new Guid("e22df771-f606-48e9-a506-fc2934f27e0e"), "5", "Role", "User", "USER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Notifies_Users_UserIdReceiver",
                table: "Notifies",
                column: "UserIdReceiver",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifies_Users_UserIdSender",
                table: "Notifies",
                column: "UserIdSender",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
