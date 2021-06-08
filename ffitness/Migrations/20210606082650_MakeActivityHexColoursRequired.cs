using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ffitness.Migrations
{
    public partial class MakeActivityHexColoursRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        { 
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
        }
    }
}
