using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShopDQ.App.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("03e769a3-a3fa-49b2-9594-7c4f548ce533"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("88df8b94-df09-4154-84d5-7c934900e224"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("97ecef3e-f964-4b65-9173-2274f0863352"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("d38c60aa-ab43-438a-b739-0cbae99b6eb7"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("e53219ef-b101-41dd-b593-f28ff12901cd"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("3382dc27-a630-4621-836e-ef734cf25463"), "4", "Role", "Seller", "SELLER" },
                    { new Guid("536f4c48-4f0c-4dad-928c-5a550fc879cf"), "2", "Role", "Manager", "MANAGER" },
                    { new Guid("7eb3eefb-6519-43d1-a50d-6ae466b03efb"), "1", "Role", "Admin", "ADMIN" },
                    { new Guid("f637ee7f-5762-4481-ad4b-20c4df9be8bd"), "3", "Role", "Shiper", "SHIPER" },
                    { new Guid("ffe0fefc-d0b1-42a9-926a-fe87dbcae857"), "5", "Role", "User", "USER" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("3382dc27-a630-4621-836e-ef734cf25463"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("536f4c48-4f0c-4dad-928c-5a550fc879cf"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7eb3eefb-6519-43d1-a50d-6ae466b03efb"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("f637ee7f-5762-4481-ad4b-20c4df9be8bd"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("ffe0fefc-d0b1-42a9-926a-fe87dbcae857"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("03e769a3-a3fa-49b2-9594-7c4f548ce533"), "4", "Role", "Seller", "SELLER" },
                    { new Guid("88df8b94-df09-4154-84d5-7c934900e224"), "3", "Role", "Shiper", "SHIPER" },
                    { new Guid("97ecef3e-f964-4b65-9173-2274f0863352"), "2", "Role", "Manager", "MANAGER" },
                    { new Guid("d38c60aa-ab43-438a-b739-0cbae99b6eb7"), "5", "Role", "User", "USER" },
                    { new Guid("e53219ef-b101-41dd-b593-f28ff12901cd"), "1", "Role", "Admin", "ADMIN" }
                });
        }
    }
}
