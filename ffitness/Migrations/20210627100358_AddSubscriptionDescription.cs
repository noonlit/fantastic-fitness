using Microsoft.EntityFrameworkCore.Migrations;

namespace Ffitness.Migrations
{
    public partial class AddSubscriptionDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Subscriptions",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Subscriptions");
        }
    }
}
