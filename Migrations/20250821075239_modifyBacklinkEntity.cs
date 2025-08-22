using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OfficeProject.Migrations
{
    /// <inheritdoc />
    public partial class modifyBacklinkEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BacklinkUrlList_WorkTaskDetails_WorkTaskId",
                table: "BacklinkUrlList");

            migrationBuilder.DropIndex(
                name: "IX_BacklinkUrlList_WorkTaskId",
                table: "BacklinkUrlList");

            migrationBuilder.DropColumn(
                name: "WorkTaskId",
                table: "BacklinkUrlList");

            migrationBuilder.RenameColumn(
                name: "BacklinkId",
                table: "BacklinkUrlList",
                newName: "BacklinkUrlId");

            migrationBuilder.CreateTable(
                name: "BacklinkDetails",
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
                    table.PrimaryKey("PK_BacklinkDetails", x => x.BacklinkId);
                    table.ForeignKey(
                        name: "FK_BacklinkDetails_WorkTaskDetails_WorkTaskId",
                        column: x => x.WorkTaskId,
                        principalTable: "WorkTaskDetails",
                        principalColumn: "WorkTaskId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BacklinkDetails_WorkTaskId",
                table: "BacklinkDetails",
                column: "WorkTaskId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BacklinkDetails");

            migrationBuilder.RenameColumn(
                name: "BacklinkUrlId",
                table: "BacklinkUrlList",
                newName: "BacklinkId");

            migrationBuilder.AddColumn<int>(
                name: "WorkTaskId",
                table: "BacklinkUrlList",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BacklinkUrlList_WorkTaskId",
                table: "BacklinkUrlList",
                column: "WorkTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_BacklinkUrlList_WorkTaskDetails_WorkTaskId",
                table: "BacklinkUrlList",
                column: "WorkTaskId",
                principalTable: "WorkTaskDetails",
                principalColumn: "WorkTaskId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
