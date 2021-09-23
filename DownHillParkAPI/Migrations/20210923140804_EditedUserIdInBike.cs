using Microsoft.EntityFrameworkCore.Migrations;

namespace DownHillParkAPI.Migrations
{
    public partial class EditedUserIdInBike : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bikes_AspNetUsers_UserId1",
                table: "Bikes");

            migrationBuilder.DropIndex(
                name: "IX_Bikes_UserId1",
                table: "Bikes");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Bikes");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Bikes",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bikes_UserId",
                table: "Bikes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bikes_AspNetUsers_UserId",
                table: "Bikes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bikes_AspNetUsers_UserId",
                table: "Bikes");

            migrationBuilder.DropIndex(
                name: "IX_Bikes_UserId",
                table: "Bikes");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Bikes",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Bikes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bikes_UserId1",
                table: "Bikes",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Bikes_AspNetUsers_UserId1",
                table: "Bikes",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
