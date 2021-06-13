using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ffitness.Migrations
{
    public partial class UpdateSubscription2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c7fb8186-3bf7-4607-98ae-87b4fb84fd60");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cc4e3e3f-35dc-40b3-ab2c-13ca37aaed39");

            migrationBuilder.AddColumn<int>(
                name: "SubscriptionId",
                table: "Bookings",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "d06349da-bb58-4ab4-93e2-80696c15ebcb", "f6000889-d62d-463f-a3fe-a45e190dd203", "UserRole", "User", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "f5bcbc67-d8f3-4eb1-bfd9-0ca7e38da139", "e3888f9c-e148-4b27-8d85-fb181628d50f", "UserRole", "Admin", null });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_SubscriptionId",
                table: "Bookings",
                column: "SubscriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Subscriptions_SubscriptionId",
                table: "Bookings",
                column: "SubscriptionId",
                principalTable: "Subscriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Subscriptions_SubscriptionId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_SubscriptionId",
                table: "Bookings");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d06349da-bb58-4ab4-93e2-80696c15ebcb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f5bcbc67-d8f3-4eb1-bfd9-0ca7e38da139");

            migrationBuilder.DropColumn(
                name: "SubscriptionId",
                table: "Bookings");

            

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "cc4e3e3f-35dc-40b3-ab2c-13ca37aaed39", "0e2f74cf-207e-457d-8bf9-7d19b129db19", "UserRole", "User", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "c7fb8186-3bf7-4607-98ae-87b4fb84fd60", "d8366c78-2cac-479c-944a-966e8a9b1cd0", "UserRole", "Admin", null });
        }
    }
}
