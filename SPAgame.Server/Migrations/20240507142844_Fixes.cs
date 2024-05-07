using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPAgame.Server.Migrations
{
    /// <inheritdoc />
    public partial class Fixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_BlackjackGames_BlackjackGameId",
                table: "Cards");

            migrationBuilder.DropIndex(
                name: "IX_Cards_BlackjackGameId",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "BlackjackGameId",
                table: "Cards");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BlackjackGameId",
                table: "Cards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cards_BlackjackGameId",
                table: "Cards",
                column: "BlackjackGameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_BlackjackGames_BlackjackGameId",
                table: "Cards",
                column: "BlackjackGameId",
                principalTable: "BlackjackGames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
