using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ffitness.Migrations
{
    public partial class TestMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookedScheduledActivity");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01845c08-4c19-4705-9b56-1ea0d3dc8f1f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ce02e949-038d-4eee-9653-983eb381ff23");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "56586368-05ea-458f-bf63-80b8e61ef511", "11a0cfeb-564b-4397-8ea3-9ce3ff0ecf21", "UserRole", "User", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "9675eece-2789-4d53-bd5f-34ec753b0c1d", "8e2e666b-73e5-4e2b-ad0a-72984683928e", "UserRole", "Admin", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "56586368-05ea-458f-bf63-80b8e61ef511");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9675eece-2789-4d53-bd5f-34ec753b0c1d");

            migrationBuilder.CreateTable(
                name: "BookedScheduledActivity",
                columns: table => new
                {
                    ActivityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BookedSpots = table.Column<int>(type: "int", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    RemainingSpots = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrainerName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "01845c08-4c19-4705-9b56-1ea0d3dc8f1f", "2943acb2-e71c-4d1e-af5e-4bda344569ec", "UserRole", "User", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "ce02e949-038d-4eee-9653-983eb381ff23", "b04f94eb-86ae-4a05-846a-09bd511a7977", "UserRole", "Admin", null });
        }
    }
}
