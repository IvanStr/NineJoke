using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NineJoke.Data.Migrations
{
    public partial class ReportTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReportPosts",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    Reason = table.Column<string>(nullable: true),
                    UploaderId = table.Column<string>(nullable: true),
                    ReporterId = table.Column<string>(nullable: true),
                    PostId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportPosts_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReportPosts_AspNetUsers_ReporterId",
                        column: x => x.ReporterId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReportPosts_AspNetUsers_UploaderId",
                        column: x => x.UploaderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReportPosts_PostId",
                table: "ReportPosts",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportPosts_ReporterId",
                table: "ReportPosts",
                column: "ReporterId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportPosts_UploaderId",
                table: "ReportPosts",
                column: "UploaderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReportPosts");
        }
    }
}
