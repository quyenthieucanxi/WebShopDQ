using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShopDQ.App.Migrations
{
    public partial class ConfigNotify : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifies_Users_UserID",
                table: "Notifies");


            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Notifies",
                newName: "UserIdSender");

            migrationBuilder.RenameIndex(
                name: "IX_Notifies_UserID",
                table: "Notifies",
                newName: "IX_Notifies_UserIdSender");

            migrationBuilder.AddColumn<Guid>(
                name: "UserIdReceiver",
                table: "Notifies",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            

            migrationBuilder.CreateIndex(
                name: "IX_Notifies_UserIdReceiver",
                table: "Notifies",
                column: "UserIdReceiver");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifies_Users_UserIdReceiver",
                table: "Notifies",
                column: "UserIdReceiver",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifies_Users_UserIdSender",
                table: "Notifies",
                column: "UserIdSender",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifies_Users_UserIdReceiver",
                table: "Notifies");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifies_Users_UserIdSender",
                table: "Notifies");

            migrationBuilder.DropIndex(
                name: "IX_Notifies_UserIdReceiver",
                table: "Notifies");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("3c0a0abe-1e17-45f4-b4d7-a33e8b005774"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4c8bf438-04e7-43c2-b0b8-8c234b9f5356"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("74d52502-0889-4a23-9ac7-51acae9e62f1"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("836fc6f2-20ee-431e-90b5-144fddb03d99"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("e22df771-f606-48e9-a506-fc2934f27e0e"));

            migrationBuilder.DropColumn(
                name: "UserIdReceiver",
                table: "Notifies");

            migrationBuilder.RenameColumn(
                name: "UserIdSender",
                table: "Notifies",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Notifies_UserIdSender",
                table: "Notifies",
                newName: "IX_Notifies_UserID");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("33037256-5b4b-4f69-82db-0a0f659f7a03"), "5", "Role", "User", "USER" },
                    { new Guid("57b4b322-3dab-4cab-bb43-337ea32709fe"), "3", "Role", "Shiper", "SHIPER" },
                    { new Guid("7d65c211-f9a0-42f8-afb1-ec4aaa6cfa5d"), "2", "Role", "Manager", "MANAGER" },
                    { new Guid("c213127c-c4ed-48a9-adbf-f4fddd662584"), "4", "Role", "Seller", "SELLER" },
                    { new Guid("db6c26ec-05ad-4d8c-b270-88119f93f900"), "1", "Role", "Admin", "ADMIN" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Notifies_Users_UserID",
                table: "Notifies",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
