using Microsoft.EntityFrameworkCore.Migrations;

namespace NineJoke.Data.Migrations
{
    public partial class AddedVoteCountToComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VoteCount",
                table: "Comments",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VoteCount",
                table: "Comments");
        }
    }
}
