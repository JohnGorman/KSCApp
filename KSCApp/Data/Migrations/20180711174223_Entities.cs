using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KSCApp.Data.Migrations
{
    public partial class Entities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNo",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.CreateTable(
                name: "League",
                columns: table => new
                {
                    LeagueId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LeagueName = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    NoOfLevels = table.Column<int>(nullable: false),
                    FixturesMade = table.Column<bool>(nullable: false),
                    LeagueType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_League", x => x.LeagueId);
                });

            migrationBuilder.CreateTable(
                name: "LevelTime",
                columns: table => new
                {
                    LevelTimeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Level = table.Column<int>(nullable: false),
                    StartTime = table.Column<TimeSpan>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LevelTime", x => x.LevelTimeId);
                });

            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    PlayerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PlayerName = table.Column<string>(nullable: false),
                    Rank = table.Column<int>(nullable: true),
                    PlayerStatus = table.Column<int>(nullable: false),
                    PlayingLeague = table.Column<bool>(nullable: false),
                    PlayerType = table.Column<int>(nullable: false),
                    ContactNo = table.Column<string>(nullable: false),
                    ProfilePicture = table.Column<byte[]>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    KSCAppUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.PlayerId);
                    table.ForeignKey(
                        name: "FK_Player_AspNetUsers_KSCAppUserId",
                        column: x => x.KSCAppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    TeamId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TeamName = table.Column<string>(nullable: true),
                    TeamNo = table.Column<int>(nullable: false),
                    TeamLogo = table.Column<byte[]>(nullable: true),
                    LeagueId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.TeamId);
                    table.ForeignKey(
                        name: "FK_Team_League_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "League",
                        principalColumn: "LeagueId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RankHistory",
                columns: table => new
                {
                    RankHistoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PlayerId = table.Column<int>(nullable: false),
                    OldRank = table.Column<int>(nullable: false),
                    NewRank = table.Column<int>(nullable: false),
                    DateChanged = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RankHistory", x => x.RankHistoryId);
                    table.ForeignKey(
                        name: "FK_RankHistory_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fixture",
                columns: table => new
                {
                    FixtureId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LeagueId = table.Column<int>(nullable: false),
                    TeamAId = table.Column<int>(nullable: false),
                    TeamBId = table.Column<int>(nullable: false),
                    PlayDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fixture", x => x.FixtureId);
                    table.ForeignKey(
                        name: "FK_Fixture_League_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "League",
                        principalColumn: "LeagueId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Fixture_Team_TeamAId",
                        column: x => x.TeamAId,
                        principalTable: "Team",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Fixture_Team_TeamBId",
                        column: x => x.TeamBId,
                        principalTable: "Team",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "TeamPlayer",
                columns: table => new
                {
                    TeamPlayerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Level = table.Column<int>(nullable: false),
                    MatchesPlayed = table.Column<int>(nullable: true),
                    MatchesWon = table.Column<int>(nullable: true),
                    GamesWon = table.Column<int>(nullable: true),
                    GamesLost = table.Column<int>(nullable: true),
                    TeamId = table.Column<int>(nullable: false),
                    PlayerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamPlayer", x => x.TeamPlayerId);
                    table.ForeignKey(
                        name: "FK_TeamPlayer_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamPlayer_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Match",
                columns: table => new
                {
                    MatchId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FixtureId = table.Column<int>(nullable: true),
                    PlayerAId = table.Column<int>(nullable: false),
                    PlayerBId = table.Column<int>(nullable: false),
                    Level = table.Column<int>(nullable: true),
                    Played = table.Column<bool>(nullable: true),
                    PlayedDate = table.Column<DateTime>(nullable: true),
                    PlayerAgames = table.Column<int>(nullable: false),
                    PlayerBgames = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Match", x => x.MatchId);
                    table.ForeignKey(
                        name: "FK_Match_Fixture_FixtureId",
                        column: x => x.FixtureId,
                        principalTable: "Fixture",
                        principalColumn: "FixtureId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Match_Player_PlayerAId",
                        column: x => x.PlayerAId,
                        principalTable: "Player",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Match_Player_PlayerBId",
                        column: x => x.PlayerBId,
                        principalTable: "Player",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "GameResult",
                columns: table => new
                {
                    GameResultId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MatchId = table.Column<int>(nullable: false),
                    GameNo = table.Column<int>(nullable: false),
                    PlayerApoints = table.Column<int>(nullable: false),
                    PlayerBpoints = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameResult", x => x.GameResultId);
                    table.ForeignKey(
                        name: "FK_GameResult_Match_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Match",
                        principalColumn: "MatchId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatchSlot",
                columns: table => new
                {
                    MatchSlotId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MatchId = table.Column<int>(nullable: true),
                    MyProperty = table.Column<DateTime>(nullable: false),
                    SlotMinutes = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchSlot", x => x.MatchSlotId);
                    table.ForeignKey(
                        name: "FK_MatchSlot_Match_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Match",
                        principalColumn: "MatchId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fixture_LeagueId",
                table: "Fixture",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_Fixture_TeamAId",
                table: "Fixture",
                column: "TeamAId");

            migrationBuilder.CreateIndex(
                name: "IX_Fixture_TeamBId",
                table: "Fixture",
                column: "TeamBId");

            migrationBuilder.CreateIndex(
                name: "IX_GameResult_MatchId",
                table: "GameResult",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Match_FixtureId",
                table: "Match",
                column: "FixtureId");

            migrationBuilder.CreateIndex(
                name: "IX_Match_PlayerAId",
                table: "Match",
                column: "PlayerAId");

            migrationBuilder.CreateIndex(
                name: "IX_Match_PlayerBId",
                table: "Match",
                column: "PlayerBId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchSlot_MatchId",
                table: "MatchSlot",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Player_KSCAppUserId",
                table: "Player",
                column: "KSCAppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RankHistory_PlayerId",
                table: "RankHistory",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Team_LeagueId",
                table: "Team",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamPlayer_PlayerId",
                table: "TeamPlayer",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamPlayer_TeamId",
                table: "TeamPlayer",
                column: "TeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameResult");

            migrationBuilder.DropTable(
                name: "LevelTime");

            migrationBuilder.DropTable(
                name: "MatchSlot");

            migrationBuilder.DropTable(
                name: "RankHistory");

            migrationBuilder.DropTable(
                name: "TeamPlayer");

            migrationBuilder.DropTable(
                name: "Match");

            migrationBuilder.DropTable(
                name: "Fixture");

            migrationBuilder.DropTable(
                name: "Player");

            migrationBuilder.DropTable(
                name: "Team");

            migrationBuilder.DropTable(
                name: "League");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PhoneNo",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
