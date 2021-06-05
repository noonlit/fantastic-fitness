using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ffitness.Migrations
{
    public partial class AddActivityValidators : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3a0d043a-b64a-4a21-add5-b81354ab2b85");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "93d324d3-1140-4468-bb85-6252110b6702");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "2660f3be-89de-46dc-8a29-d8e29fcb99fe", "2e4a853e-0883-4648-8b08-7bbde3f17874", "UserRole", "AppUser", "APPUSER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "53dcf167-c88e-403d-9572-83a84a07b4ec", "f6a023e7-2cab-4a12-993a-0be99352211f", "UserRole", "AppAdmin", "APPADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2660f3be-89de-46dc-8a29-d8e29fcb99fe");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "53dcf167-c88e-403d-9572-83a84a07b4ec");

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
                values: new object[] { "93d324d3-1140-4468-bb85-6252110b6702", "1b330914-1753-4e98-b829-d58244c8eb5e", "UserRole", "AppUser", "APPUSER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "3a0d043a-b64a-4a21-add5-b81354ab2b85", "429edf52-0c6c-43d0-9eaf-4f1bb1fe8adf", "UserRole", "AppAdmin", "APPADMIN" });
        }
    }
}
