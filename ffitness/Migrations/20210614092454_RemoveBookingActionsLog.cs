using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ffitness.Migrations
{
    public partial class RemoveBookingActionsLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserActionsBooking");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserActionsBooking",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ScheduledActivityId = table.Column<int>(type: "int", nullable: false),
                    UserAction = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserActionsBooking", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserActionsBooking_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserActionsBooking_ScheduledActivities_ScheduledActivityId",
                        column: x => x.ScheduledActivityId,
                        principalTable: "ScheduledActivities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserActionsBooking_ScheduledActivityId",
                table: "UserActionsBooking",
                column: "ScheduledActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_UserActionsBooking_UserId",
                table: "UserActionsBooking",
                column: "UserId");
        }
    }
}
