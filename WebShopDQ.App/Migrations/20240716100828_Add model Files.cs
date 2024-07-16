using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShopDQ.App.Migrations
{
    public partial class AddmodelFiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
       
            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    productID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Files_Posts_productID",
                        column: x => x.productID,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Files_productID",
                table: "Files",
                column: "productID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("06c0c362-ddf6-4272-83ef-9efcd6cc2119"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("30c00e58-51a9-49ea-92e2-f544a960ab01"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("977f9b67-f5b2-41b6-ad89-633cf982d372"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("a6398c84-b210-4f6f-954b-f853f17b9ac7"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("b9c10aa3-58f9-4480-8353-fa1e212cf360"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("30d28771-22f5-4074-9cc5-b99dffadad12"), "1", "Role", "Admin", "ADMIN" },
                    { new Guid("5bdd7573-9b90-4d5b-b5da-579a217e4fd3"), "5", "Role", "User", "USER" },
                    { new Guid("6d61a132-66b7-4dea-890a-aafe03877693"), "2", "Role", "Manager", "MANAGER" },
                    { new Guid("a10753b5-8214-4082-b422-ac48834c9385"), "4", "Role", "Seller", "SELLER" },
                    { new Guid("aeb0e8e1-5f10-4b83-9811-a3db6ea7a1f4"), "3", "Role", "Shiper", "SHIPER" }
                });
        }
    }
}
