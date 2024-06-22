using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShopDQ.App.Migrations
{
    public partial class FixFollower : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friendships_Users_FollowerID",
                table: "Friendships");

            migrationBuilder.DropForeignKey(
                name: "FK_Friendships_Users_FollowingID",
                table: "Friendships");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Friendships",
                table: "Friendships");

            migrationBuilder.DropIndex(
                name: "IX_Friendships_FollowingID",
                table: "Friendships");

            

            migrationBuilder.AddPrimaryKey(
                name: "PK_Friendships",
                table: "Friendships",
                columns: new[] { "FollowingID", "FollowerID" });

            migrationBuilder.AddForeignKey(
                name: "FK_Friendships_Users_FollowerID",
                table: "Friendships",
                column: "FollowerID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Friendships_Users_FollowingID",
                table: "Friendships",
                column: "FollowingID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friendships_Users_FollowerID",
                table: "Friendships");

            migrationBuilder.DropForeignKey(
                name: "FK_Friendships_Users_FollowingID",
                table: "Friendships");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Friendships",
                table: "Friendships");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("322c9a68-9945-4882-b805-e26d6582f077"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("36399ab6-2294-482f-b7a1-828cc960de45"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("d964a905-13c0-44d8-b837-59d99b51e347"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("f4826d5e-8096-48a8-b52c-cab79bf0843c"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("fdfe1a75-69db-4309-a0cb-76db390add13"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Friendships",
                table: "Friendships",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("15538e96-ff87-4954-bd9e-7606b7583ff1"), "4", "Role", "Seller", "SELLER" },
                    { new Guid("333cdde3-4acd-4d65-a56e-f5271a9eee22"), "1", "Role", "Admin", "ADMIN" },
                    { new Guid("878599e8-560e-47a4-bddc-67f24a54177a"), "2", "Role", "Manager", "MANAGER" },
                    { new Guid("bc7436a4-9f55-4f3b-b463-f98f52086010"), "3", "Role", "Shiper", "SHIPER" },
                    { new Guid("d1f159ae-29b2-4819-9b54-6b09d3b91910"), "5", "Role", "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_FollowingID",
                table: "Friendships",
                column: "FollowingID");

            migrationBuilder.AddForeignKey(
                name: "FK_Friendships_Users_FollowerID",
                table: "Friendships",
                column: "FollowerID",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Friendships_Users_FollowingID",
                table: "Friendships",
                column: "FollowingID",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
