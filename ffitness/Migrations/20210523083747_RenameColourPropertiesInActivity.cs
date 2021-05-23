using Microsoft.EntityFrameworkCore.Migrations;

namespace Ffitness.Migrations
{
    public partial class RenameColourPropertiesInActivity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Colour",
                table: "Activities",
                newName: "PrimaryColour");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PrimaryColour",
                table: "Activities",
                newName: "Colour");
        }
    }
}
