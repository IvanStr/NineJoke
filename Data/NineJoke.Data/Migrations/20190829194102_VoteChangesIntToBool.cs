using Microsoft.EntityFrameworkCore.Migrations;

namespace NineJoke.Data.Migrations
{
    public partial class VoteChangesIntToBool : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "UpOrDown",
                table: "VotePosts",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<bool>(
                name: "UpOrDown",
                table: "VoteComments",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UpOrDown",
                table: "VotePosts",
                nullable: false,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<int>(
                name: "UpOrDown",
                table: "VoteComments",
                nullable: false,
                oldClrType: typeof(bool));
        }
    }
}
