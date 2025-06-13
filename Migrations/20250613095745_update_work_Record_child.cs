using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OfficeProject.Migrations
{
    /// <inheritdoc />
    public partial class update_work_Record_child : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "TaskDate",
                table: "SeoTaskDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "OthersTaskDetails",
                columns: table => new
                {
                    OthersTaskId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkRecordId = table.Column<int>(type: "int", nullable: false),
                    LableName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    SharedPost = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TaskDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OthersTaskDetails", x => x.OthersTaskId);
                    table.ForeignKey(
                        name: "FK_OthersTaskDetails_WorkRecords_WorkRecordId",
                        column: x => x.WorkRecordId,
                        principalTable: "WorkRecords",
                        principalColumn: "WorkRecordId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WebDeveTaskDetails",
                columns: table => new
                {
                    webDevTaskId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkRecordId = table.Column<int>(type: "int", nullable: false),
                    Task = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaskDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebDeveTaskDetails", x => x.webDevTaskId);
                    table.ForeignKey(
                        name: "FK_WebDeveTaskDetails_WorkRecords_WorkRecordId",
                        column: x => x.WorkRecordId,
                        principalTable: "WorkRecords",
                        principalColumn: "WorkRecordId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OthersTaskDetails_WorkRecordId",
                table: "OthersTaskDetails",
                column: "WorkRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_WebDeveTaskDetails_WorkRecordId",
                table: "WebDeveTaskDetails",
                column: "WorkRecordId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OthersTaskDetails");

            migrationBuilder.DropTable(
                name: "WebDeveTaskDetails");

            migrationBuilder.DropColumn(
                name: "TaskDate",
                table: "SeoTaskDetails");
        }
    }
}
