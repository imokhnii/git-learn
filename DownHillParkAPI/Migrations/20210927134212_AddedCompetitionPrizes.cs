using Microsoft.EntityFrameworkCore.Migrations;

namespace DownHillParkAPI.Migrations
{
    public partial class AddedCompetitionPrizes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstPlace",
                table: "Competitions");

            migrationBuilder.DropColumn(
                name: "SecondPlace",
                table: "Competitions");

            migrationBuilder.DropColumn(
                name: "ThirdPlace",
                table: "Competitions");

            migrationBuilder.AddColumn<int>(
                name: "CompetitionPrizeId",
                table: "Competitions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CompetitionPrizes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstPlace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    secondPlace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    thirdPlace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompetitionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitionPrizes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompetitionPrizes_Competitions_CompetitionId",
                        column: x => x.CompetitionId,
                        principalTable: "Competitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionPrizes_CompetitionId",
                table: "CompetitionPrizes",
                column: "CompetitionId",
                unique: true,
                filter: "[CompetitionId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompetitionPrizes");

            migrationBuilder.DropColumn(
                name: "CompetitionPrizeId",
                table: "Competitions");

            migrationBuilder.AddColumn<string>(
                name: "FirstPlace",
                table: "Competitions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecondPlace",
                table: "Competitions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThirdPlace",
                table: "Competitions",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
