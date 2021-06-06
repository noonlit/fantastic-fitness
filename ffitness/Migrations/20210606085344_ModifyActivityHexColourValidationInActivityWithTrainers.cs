using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ffitness.Migrations
{
    public partial class ModifyActivityHexColourValidationInActivityWithTrainers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                values: new object[] { "061d000b-fe0e-4899-aef6-0fbda8a5369b", "498d1d21-3d3b-417e-ab93-21ec7c72feaf", "UserRole", "AppUser", "APPUSER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "a4cb09d9-96c1-465d-89b3-5bb4e55d5a4a", "3ba46946-811b-40b3-b423-dbe6573fe943", "UserRole", "AppAdmin", "APPADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "061d000b-fe0e-4899-aef6-0fbda8a5369b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a4cb09d9-96c1-465d-89b3-5bb4e55d5a4a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "2168f4d3-b454-4c61-9cc1-ebef8165ac2f", "86607cf8-5db3-4f9c-b29f-5c7126d6e8fb", "UserRole", "AppUser", "APPUSER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "f204dde1-ce8d-4253-a86f-90e16409e6c5", "8de8ad8e-e7ac-46d0-a8ce-f8308913a733", "UserRole", "AppAdmin", "APPADMIN" });
        }
    }
}
