using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace finalProject.Migrations
{
    public partial class seeddata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Battles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Team1Id = table.Column<int>(type: "int", nullable: false),
                    Team2Id = table.Column<int>(type: "int", nullable: false),
                    WinnerTeamId = table.Column<int>(type: "int", nullable: true),
                    BattleDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Battles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Battles_Teams_Team1Id",
                        column: x => x.Team1Id,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Battles_Teams_Team2Id",
                        column: x => x.Team2Id,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Battles_Teams_WinnerTeamId",
                        column: x => x.WinnerTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "TotalWins", "Username" },
                values: new object[] { 1, 15, "JohnDoe" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "TotalWins", "Username" },
                values: new object[] { 2, 20, "JaneDoe" });

            migrationBuilder.InsertData(
                table: "Badges",
                columns: new[] { "Id", "Description", "Name", "UserId" },
                values: new object[,]
                {
                    { 1, "Awarded for winning 10 battles", "Champion", 1 },
                    { 2, "Awarded for playing 50 battles", "Veteran", 2 }
                });

            migrationBuilder.InsertData(
                table: "Leaderboards",
                columns: new[] { "Id", "Rank", "SnapshotDate", "TotalWins", "UserId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, 1 },
                    { 2, 2, new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 20, 2 }
                });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "Name", "TotalWins", "UserId" },
                values: new object[,]
                {
                    { 1, "Madrid", 10, 1 },
                    { 2, "Barcelona", 8, 2 }
                });

            migrationBuilder.InsertData(
                table: "Battles",
                columns: new[] { "Id", "BattleDate", "Team1Id", "Team2Id", "WinnerTeamId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2, null },
                    { 2, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1, null }
                });

            migrationBuilder.InsertData(
                table: "Characters",
                columns: new[] { "Id", "Defense", "Health", "Name", "Speed", "Strength", "TeamId" },
                values: new object[,]
                {
                    { 1, 300, 300, "Drone", 400, 500, 1 },
                    { 2, 200, 300, "Flight", 350, 400, 2 }
                });

            migrationBuilder.InsertData(
                table: "Moves",
                columns: new[] { "Id", "CharacterId", "Name", "Power" },
                values: new object[] { 1, 1, "Laser Beam", 500 });

            migrationBuilder.InsertData(
                table: "Moves",
                columns: new[] { "Id", "CharacterId", "Name", "Power" },
                values: new object[] { 2, 2, "Sky Strike", 600 });

            migrationBuilder.CreateIndex(
                name: "IX_Battles_Team1Id",
                table: "Battles",
                column: "Team1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Battles_Team2Id",
                table: "Battles",
                column: "Team2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Battles_WinnerTeamId",
                table: "Battles",
                column: "WinnerTeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Battles");

            migrationBuilder.DeleteData(
                table: "Badges",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Badges",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Leaderboards",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Leaderboards",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Moves",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Moves",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
