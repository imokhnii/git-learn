using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DownHillParkAPI.Migrations
{
    public partial class AddedCompetitions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentCompetitionId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Competitions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOfEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FirstPlace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondPlace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThirdPlace = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competitions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CurrentCompetitionId",
                table: "AspNetUsers",
                column: "CurrentCompetitionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Competitions_CurrentCompetitionId",
                table: "AspNetUsers",
                column: "CurrentCompetitionId",
                principalTable: "Competitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Competitions_CurrentCompetitionId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Competitions");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CurrentCompetitionId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CurrentCompetitionId",
                table: "AspNetUsers");
        }
    }
}
