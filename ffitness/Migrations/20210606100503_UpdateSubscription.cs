using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ffitness.Migrations
{
    public partial class UpdateSubscription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7bcf0c06-ccac-4077-b479-d7608910209d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cbdaed73-e1c8-41e4-b6ea-bcf7f8f19e93");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Subscriptions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "cc4e3e3f-35dc-40b3-ab2c-13ca37aaed39", "0e2f74cf-207e-457d-8bf9-7d19b129db19", "UserRole", "User", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "c7fb8186-3bf7-4607-98ae-87b4fb84fd60", "d8366c78-2cac-479c-944a-966e8a9b1cd0", "UserRole", "Admin", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c7fb8186-3bf7-4607-98ae-87b4fb84fd60");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cc4e3e3f-35dc-40b3-ab2c-13ca37aaed39");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Subscriptions");

            

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "cbdaed73-e1c8-41e4-b6ea-bcf7f8f19e93", "8349c74e-8360-4a3b-a1ff-dfeab44a7c32", "UserRole", "User", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "7bcf0c06-ccac-4077-b479-d7608910209d", "295250d8-5382-4970-9601-c16f2a799f34", "UserRole", "Admin", null });
        }
    }
}
