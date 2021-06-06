using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ffitness.Migrations
{
    public partial class ModifyActivityHexColourValidation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "819cbdbe-b306-4e98-b225-295c9bbdfa7b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "90fe41e5-24ad-496f-871e-aa2863e32b92");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "2168f4d3-b454-4c61-9cc1-ebef8165ac2f", "86607cf8-5db3-4f9c-b29f-5c7126d6e8fb", "UserRole", "AppUser", "APPUSER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "f204dde1-ce8d-4253-a86f-90e16409e6c5", "8de8ad8e-e7ac-46d0-a8ce-f8308913a733", "UserRole", "AppAdmin", "APPADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2168f4d3-b454-4c61-9cc1-ebef8165ac2f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f204dde1-ce8d-4253-a86f-90e16409e6c5");

                       migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "819cbdbe-b306-4e98-b225-295c9bbdfa7b", "8fa19711-6dec-4e2c-8202-767e37faa308", "UserRole", "AppUser", "APPUSER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "90fe41e5-24ad-496f-871e-aa2863e32b92", "bb8f956e-1a12-401a-b534-d39c2110af7a", "UserRole", "AppAdmin", "APPADMIN" });
        }
    }
}
