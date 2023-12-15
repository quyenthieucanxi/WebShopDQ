using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShopDQ.App.Migrations
{
    public partial class fixlistPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("14e51058-1f4e-47c1-9f95-5f05ce08ebb0"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("90acc26f-9e6a-4757-a280-d1727dfa1fe4"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("cba01f2e-e178-4e58-9904-3851a22d04a4"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("ddd16bb7-6055-4478-b4b9-56dba719917d"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("feb6518b-06bc-484e-ae59-a7f3653f0cbc"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("109c283b-c20e-4761-9a6c-485481857849"), "3", "Role", "Shiper", "SHIPER" },
                    { new Guid("5be31c67-0581-4c2b-ba55-c3328f686f11"), "4", "Role", "Seller", "SELLER" },
                    { new Guid("a287a254-4668-4b45-a666-5323aa6159c7"), "2", "Role", "Manager", "MANAGER" },
                    { new Guid("cab63a99-b90d-4e9c-9630-a24b2e9c6b41"), "5", "Role", "User", "USER" },
                    { new Guid("ef7bb1ca-d27e-47c5-9a80-b9eedcf360c6"), "1", "Role", "Admin", "ADMIN" }
                });
        }
    }
}
