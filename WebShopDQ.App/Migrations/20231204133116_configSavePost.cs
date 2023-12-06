using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShopDQ.App.Migrations
{
    public partial class configSavePost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SavePosts",
                table: "SavePosts");

            migrationBuilder.DropIndex(
                name: "IX_SavePosts_UserID",
                table: "SavePosts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SavePosts",
                table: "SavePosts",
                columns: new[] { "UserID", "PostID" });

            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SavePosts",
                table: "SavePosts");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("0537e971-dcb1-476e-8eeb-f2d5d6923332"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("241df9fa-6f5a-483a-9876-418794b6ab42"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("860cc4f3-ae71-450e-af02-70c9262222d7"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("94234755-86b8-4cab-890a-e2b63f98162e"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("d5db9ce0-fd8f-45cc-b4e3-27b3cd5f23bf"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_SavePosts",
                table: "SavePosts",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("607eaa40-662c-4046-933d-4332b67b652f"), "1", "Role", "Admin", "ADMIN" },
                    { new Guid("87d69548-9c02-4516-8291-8b5edc00adea"), "4", "Role", "Seller", "SELLER" },
                    { new Guid("bbc50b02-7121-4bfe-8212-f420bf7deffc"), "5", "Role", "User", "USER" },
                    { new Guid("efbb88dd-9d8e-4642-9a53-50c2f6db508e"), "3", "Role", "Shiper", "SHIPER" },
                    { new Guid("f2f2d177-a565-468f-a93f-e49715842e51"), "2", "Role", "Manager", "MANAGER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SavePosts_UserID",
                table: "SavePosts",
                column: "UserID");
        }
    }
}
