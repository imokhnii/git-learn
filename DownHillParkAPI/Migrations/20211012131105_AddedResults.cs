using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DownHillParkAPI.Migrations
{
    public partial class AddedResults : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "CompetitionResultId",
                table: "Competitions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Competitions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CompetitionResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompetitionId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalTime = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitionResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompetitionResults_Competitions_CompetitionId",
                        column: x => x.CompetitionId,
                        principalTable: "Competitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Laps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResultId = table.Column<int>(type: "int", nullable: false),
                    LapNumber = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LapTime = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Laps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Laps_CompetitionResults_ResultId",
                        column: x => x.ResultId,
                        principalTable: "CompetitionResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionResults_CompetitionId",
                table: "CompetitionResults",
                column: "CompetitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Laps_ResultId",
                table: "Laps",
                column: "ResultId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Laps");

            migrationBuilder.DropTable(
                name: "CompetitionResults");

            migrationBuilder.DropColumn(
                name: "CompetitionResultId",
                table: "Competitions");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Competitions");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
