using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ffitness.Migrations
{
    public partial class MakeActivityHexColoursRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "#000");

            migrationBuilder.AlterColumn<string>(
                name: "PrimaryColour",
                table: "Activities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "#000");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "819cbdbe-b306-4e98-b225-295c9bbdfa7b", "8fa19711-6dec-4e2c-8202-767e37faa308", "UserRole", "AppUser", "APPUSER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "90fe41e5-24ad-496f-871e-aa2863e32b92", "bb8f956e-1a12-401a-b534-d39c2110af7a", "UserRole", "AppAdmin", "APPADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "819cbdbe-b306-4e98-b225-295c9bbdfa7b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "90fe41e5-24ad-496f-871e-aa2863e32b92");

            migrationBuilder.AlterColumn<string>(
                name: "SecondaryColour",
                table: "Activities",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "#000",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PrimaryColour",
                table: "Activities",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "#000",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "80d082bf-3c52-415a-bb08-125e59f2e6a8", "7fab2986-ef7f-4f0b-b117-3daa83b1afe2", "UserRole", "AppUser", "APPUSER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "0c768b15-dad5-48e7-96ee-117bb196ce81", "b708d593-0ffb-4f31-b95d-59c33c1c5087", "UserRole", "AppAdmin", "APPADMIN" });
        }
    }
}
