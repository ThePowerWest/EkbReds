using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class renamelogoawayteamtoawayteamlogo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LogoHomeTeam",
                table: "Matches",
                newName: "HomeTeamLogo");

            migrationBuilder.RenameColumn(
                name: "LogoAwayTeam",
                table: "Matches",
                newName: "AwayTeamLogo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HomeTeamLogo",
                table: "Matches",
                newName: "LogoHomeTeam");

            migrationBuilder.RenameColumn(
                name: "AwayTeamLogo",
                table: "Matches",
                newName: "LogoAwayTeam");
        }
    }
}
