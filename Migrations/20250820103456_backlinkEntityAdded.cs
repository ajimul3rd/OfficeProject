using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OfficeProject.Migrations
{
    /// <inheritdoc />
    public partial class backlinkEntityAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BacklinkUrlList",
                columns: table => new
                {
                    BacklinkId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkTaskId = table.Column<int>(type: "int", nullable: false),
                    SiteUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublishUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublishTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BacklinkUrlList", x => x.BacklinkId);
                    table.ForeignKey(
                        name: "FK_BacklinkUrlList_WorkTaskDetails_WorkTaskId",
                        column: x => x.WorkTaskId,
                        principalTable: "WorkTaskDetails",
                        principalColumn: "WorkTaskId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BacklinkUrlList_WorkTaskId",
                table: "BacklinkUrlList",
                column: "WorkTaskId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BacklinkUrlList");
        }
    }
}
