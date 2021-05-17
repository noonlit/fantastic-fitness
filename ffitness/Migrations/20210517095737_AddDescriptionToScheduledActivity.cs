using Microsoft.EntityFrameworkCore.Migrations;

namespace Ffitness.Migrations
{
    public partial class AddDescriptionToScheduledActivity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ScheduledActivities",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "ScheduledActivities");
        }
    }
}
