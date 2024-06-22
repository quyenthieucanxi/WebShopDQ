using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShopDQ.App.Migrations
{
    public partial class FixOrderReview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostReviews_Posts_PostId",
                table: "PostReviews");

           

            migrationBuilder.RenameColumn(
                name: "PostId",
                table: "PostReviews",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_PostReviews_PostId",
                table: "PostReviews",
                newName: "IX_PostReviews_OrderId");

           

            migrationBuilder.AddForeignKey(
                name: "FK_PostReviews_Orders_OrderId",
                table: "PostReviews",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostReviews_Orders_OrderId",
                table: "PostReviews");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("44acf989-626f-4344-b48f-7d44eb662b89"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("5453f200-eef3-4016-b836-c068767cecde"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("96942997-821a-4246-869c-c5dcfdb0dc65"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9c2183cb-0c60-4c20-bd81-d9d387eae9d6"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("ac9c722f-cd64-4b77-ba9d-d8808f980318"));

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "PostReviews",
                newName: "PostId");

            migrationBuilder.RenameIndex(
                name: "IX_PostReviews_OrderId",
                table: "PostReviews",
                newName: "IX_PostReviews_PostId");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("322c9a68-9945-4882-b805-e26d6582f077"), "5", "Role", "User", "USER" },
                    { new Guid("36399ab6-2294-482f-b7a1-828cc960de45"), "2", "Role", "Manager", "MANAGER" },
                    { new Guid("d964a905-13c0-44d8-b837-59d99b51e347"), "1", "Role", "Admin", "ADMIN" },
                    { new Guid("f4826d5e-8096-48a8-b52c-cab79bf0843c"), "4", "Role", "Seller", "SELLER" },
                    { new Guid("fdfe1a75-69db-4309-a0cb-76db390add13"), "3", "Role", "Shiper", "SHIPER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_PostReviews_Posts_PostId",
                table: "PostReviews",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
