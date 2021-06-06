using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ffitness.Migrations
{
    public partial class ModifyActivityValidators : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "83cd215b-510f-4163-93cd-4a430f844ff7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9c0ca904-d0c8-4742-b980-6f91dbad3d01");



            migrationBuilder.AlterColumn<string>(
                name: "SecondaryColour",
                table: "Activities",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "#000",
                oldClrType: typeof(string),
                oldType: "nvarchar(7)",
                oldMaxLength: 7,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PrimaryColour",
                table: "Activities",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "#000",
                oldClrType: typeof(string),
                oldType: "nvarchar(7)",
                oldMaxLength: 7,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Activities",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "ActivityPicture",
                table: "Activities",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "default-activity-picture.jpg",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "80d082bf-3c52-415a-bb08-125e59f2e6a8", "7fab2986-ef7f-4f0b-b117-3daa83b1afe2", "UserRole", "AppUser", "APPUSER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "0c768b15-dad5-48e7-96ee-117bb196ce81", "b708d593-0ffb-4f31-b95d-59c33c1c5087", "UserRole", "AppAdmin", "APPADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0c768b15-dad5-48e7-96ee-117bb196ce81");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "80d082bf-3c52-415a-bb08-125e59f2e6a8");

            migrationBuilder.AlterColumn<string>(
                name: "SecondaryColour",
                table: "Activities",
                type: "nvarchar(7)",
                maxLength: 7,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "#000");

            migrationBuilder.AlterColumn<string>(
                name: "PrimaryColour",
                table: "Activities",
                type: "nvarchar(7)",
                maxLength: 7,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "#000");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Activities",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ActivityPicture",
                table: "Activities",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "default-activity-picture.jpg");

            migrationBuilder.CreateTable(
                name: "BookedScheduledActivity",
                columns: table => new
                {
                    ActivityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BookedSpots = table.Column<int>(type: "int", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                values: new object[] { "9c0ca904-d0c8-4742-b980-6f91dbad3d01", "64f0bc8e-5a41-4569-8f57-a0c8c3453b9d", "UserRole", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "83cd215b-510f-4163-93cd-4a430f844ff7", "d2f2827c-83a6-4b8c-ae63-bcc36a932a6d", "UserRole", "Admin", "USER" });
        }
    }
}
