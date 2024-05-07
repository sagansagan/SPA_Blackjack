using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPAgame.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddedTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HighScore",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BlackjackGames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlackjackGames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlackjackGames_AspNetUsers_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DealerHands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlackjackGameId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DealerHands", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DealerHands_BlackjackGames_BlackjackGameId",
                        column: x => x.BlackjackGameId,
                        principalTable: "BlackjackGames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerHands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlackjackGameId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerHands", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerHands_BlackjackGames_BlackjackGameId",
                        column: x => x.BlackjackGameId,
                        principalTable: "BlackjackGames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Suit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rank = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BlackjackGameId = table.Column<int>(type: "int", nullable: false),
                    DealerHandId = table.Column<int>(type: "int", nullable: true),
                    PlayerHandId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cards_BlackjackGames_BlackjackGameId",
                        column: x => x.BlackjackGameId,
                        principalTable: "BlackjackGames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cards_DealerHands_DealerHandId",
                        column: x => x.DealerHandId,
                        principalTable: "DealerHands",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cards_PlayerHands_PlayerHandId",
                        column: x => x.PlayerHandId,
                        principalTable: "PlayerHands",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlackjackGames_PlayerId",
                table: "BlackjackGames",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_BlackjackGameId",
                table: "Cards",
                column: "BlackjackGameId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_DealerHandId",
                table: "Cards",
                column: "DealerHandId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_PlayerHandId",
                table: "Cards",
                column: "PlayerHandId");

            migrationBuilder.CreateIndex(
                name: "IX_DealerHands_BlackjackGameId",
                table: "DealerHands",
                column: "BlackjackGameId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlayerHands_BlackjackGameId",
                table: "PlayerHands",
                column: "BlackjackGameId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "DealerHands");

            migrationBuilder.DropTable(
                name: "PlayerHands");

            migrationBuilder.DropTable(
                name: "BlackjackGames");

            migrationBuilder.DropColumn(
                name: "HighScore",
                table: "AspNetUsers");
        }
    }
}
