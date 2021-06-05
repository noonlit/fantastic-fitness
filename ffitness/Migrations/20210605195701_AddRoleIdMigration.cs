using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ffitness.Migrations
{
    public partial class AddRoleIdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_AspNetUsers_UserId",
                table: "Bookings");

        //    migrationBuilder.DropTable(
        //        name: "BookedScheduledActivity");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_UserId_ScheduledActivityId",
                table: "Bookings");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2660f3be-89de-46dc-8a29-d8e29fcb99fe");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "53dcf167-c88e-403d-9572-83a84a07b4ec");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Bookings",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "01845c08-4c19-4705-9b56-1ea0d3dc8f1f", "2943acb2-e71c-4d1e-af5e-4bda344569ec", "UserRole", "User", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "ce02e949-038d-4eee-9653-983eb381ff23", "b04f94eb-86ae-4a05-846a-09bd511a7977", "UserRole", "Admin", null });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserId_ScheduledActivityId",
                table: "Bookings",
                columns: new[] { "UserId", "ScheduledActivityId" },
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_AspNetUsers_UserId",
                table: "Bookings",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_AspNetUsers_UserId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_UserId_ScheduledActivityId",
                table: "Bookings");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01845c08-4c19-4705-9b56-1ea0d3dc8f1f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ce02e949-038d-4eee-9653-983eb381ff23");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Bookings",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

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
                values: new object[] { "2660f3be-89de-46dc-8a29-d8e29fcb99fe", "2e4a853e-0883-4648-8b08-7bbde3f17874", "UserRole", "AppUser", "APPUSER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "53dcf167-c88e-403d-9572-83a84a07b4ec", "f6a023e7-2cab-4a12-993a-0be99352211f", "UserRole", "AppAdmin", "APPADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserId_ScheduledActivityId",
                table: "Bookings",
                columns: new[] { "UserId", "ScheduledActivityId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_AspNetUsers_UserId",
                table: "Bookings",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
