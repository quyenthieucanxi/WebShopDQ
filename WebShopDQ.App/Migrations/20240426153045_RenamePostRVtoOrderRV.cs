using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShopDQ.App.Migrations
{
    public partial class RenamePostRVtoOrderRV : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostReviews_Orders_OrderId",
                table: "PostReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_PostReviews_Users_UserId",
                table: "PostReviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostReviews",
                table: "PostReviews");

            

            migrationBuilder.RenameTable(
                name: "PostReviews",
                newName: "OrderReviews");

            migrationBuilder.RenameIndex(
                name: "IX_PostReviews_UserId",
                table: "OrderReviews",
                newName: "IX_OrderReviews_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_PostReviews_OrderId",
                table: "OrderReviews",
                newName: "IX_OrderReviews_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderReviews",
                table: "OrderReviews",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderReviews_Orders_OrderId",
                table: "OrderReviews",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderReviews_Users_UserId",
                table: "OrderReviews",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderReviews_Orders_OrderId",
                table: "OrderReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderReviews_Users_UserId",
                table: "OrderReviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderReviews",
                table: "OrderReviews");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("3d5b2ca9-fb54-4b4d-bf91-383d1460c19b"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("760bf101-8299-47c2-878c-4835c638b897"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("ab713fdd-8451-44b9-9eab-128d7e7dba0a"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("acf390a4-6cd3-4b01-81da-ff9726678a57"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("b15ec1a3-342f-4e9e-bb36-b0cba16ad513"));

            migrationBuilder.RenameTable(
                name: "OrderReviews",
                newName: "PostReviews");

            migrationBuilder.RenameIndex(
                name: "IX_OrderReviews_UserId",
                table: "PostReviews",
                newName: "IX_PostReviews_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderReviews_OrderId",
                table: "PostReviews",
                newName: "IX_PostReviews_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostReviews",
                table: "PostReviews",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("44acf989-626f-4344-b48f-7d44eb662b89"), "3", "Role", "Shiper", "SHIPER" },
                    { new Guid("5453f200-eef3-4016-b836-c068767cecde"), "5", "Role", "User", "USER" },
                    { new Guid("96942997-821a-4246-869c-c5dcfdb0dc65"), "2", "Role", "Manager", "MANAGER" },
                    { new Guid("9c2183cb-0c60-4c20-bd81-d9d387eae9d6"), "4", "Role", "Seller", "SELLER" },
                    { new Guid("ac9c722f-cd64-4b77-ba9d-d8808f980318"), "1", "Role", "Admin", "ADMIN" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_PostReviews_Orders_OrderId",
                table: "PostReviews",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostReviews_Users_UserId",
                table: "PostReviews",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
