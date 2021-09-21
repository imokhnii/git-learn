using Microsoft.EntityFrameworkCore.Migrations;

namespace DownHillParkAPI.Migrations
{
    public partial class EditedCompetitions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Competitions_CurrentCompetitionId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "CurrentCompetitionId",
                table: "AspNetUsers",
                newName: "CompetitionId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_CurrentCompetitionId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_CompetitionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Competitions_CompetitionId",
                table: "AspNetUsers",
                column: "CompetitionId",
                principalTable: "Competitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Competitions_CompetitionId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "CompetitionId",
                table: "AspNetUsers",
                newName: "CurrentCompetitionId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_CompetitionId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_CurrentCompetitionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Competitions_CurrentCompetitionId",
                table: "AspNetUsers",
                column: "CurrentCompetitionId",
                principalTable: "Competitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
