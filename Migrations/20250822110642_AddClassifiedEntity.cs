using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OfficeProject.Migrations
{
    /// <inheritdoc />
    public partial class AddClassifiedEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClasdifiedDetails",
                columns: table => new
                {
                    ClassifiedId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BacklinkUrlId = table.Column<int>(type: "int", nullable: false),
                    WorkTask_Id = table.Column<int>(type: "int", nullable: false),
                    SiteUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublishUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublishTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClasdifiedDetails", x => x.ClassifiedId);
                    table.ForeignKey(
                        name: "FK_ClasdifiedDetails_WorkTaskDetails_WorkTask_Id",
                        column: x => x.WorkTask_Id,
                        principalTable: "WorkTaskDetails",
                        principalColumn: "WorkTaskId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClasdifiedDetails_WorkTask_Id",
                table: "ClasdifiedDetails",
                column: "WorkTask_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClasdifiedDetails");
        }
    }
}
