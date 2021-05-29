using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ffitness.Migrations
{
    public partial class AddUserActionsBooking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserActionsBooking",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserAction = table.Column<int>(type: "int", nullable: false),
                    ScheduledActivityId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserActionsBooking");
        }
    }
}
