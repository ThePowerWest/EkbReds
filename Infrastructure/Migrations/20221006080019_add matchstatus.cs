using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class addmatchstatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "MatchStatusId",
                table: "Matches",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.CreateTable(
                name: "MatchStatuses",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchStatuses", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "MatchStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[] { (byte)0, "NotStarted" });

            migrationBuilder.InsertData(
                table: "MatchStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[] { (byte)1, "Finished" });

            migrationBuilder.InsertData(
                table: "MatchStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[] { (byte)2, "Postponed" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MatchStatuses");

            migrationBuilder.DropColumn(
                name: "MatchStatusId",
                table: "Matches");
        }
    }
}
