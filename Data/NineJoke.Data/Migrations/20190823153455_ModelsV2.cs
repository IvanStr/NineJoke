using Microsoft.EntityFrameworkCore.Migrations;

namespace NineJoke.Data.Migrations
{
    public partial class ModelsV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReportCount",
                table: "Posts",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReportCount",
                table: "Posts");
        }
    }
}
