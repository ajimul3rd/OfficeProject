using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OfficeProject.Migrations
{
    /// <inheritdoc />
    public partial class modify_workRecordId_to_workTaskId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OthersTaskDetails_WorkTaskDetails_WorkRecordId",
                table: "OthersTaskDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_SeoTaskDetails_WorkTaskDetails_WorkRecordId",
                table: "SeoTaskDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_WebDeveTaskDetails_WorkTaskDetails_WorkRecordId",
                table: "WebDeveTaskDetails");

            migrationBuilder.RenameColumn(
                name: "WorkRecordId",
                table: "WorkTaskDetails",
                newName: "WorkTaskId");

            migrationBuilder.RenameColumn(
                name: "WorkRecordId",
                table: "WebDeveTaskDetails",
                newName: "WorkTaskId");

            migrationBuilder.RenameIndex(
                name: "IX_WebDeveTaskDetails_WorkRecordId",
                table: "WebDeveTaskDetails",
                newName: "IX_WebDeveTaskDetails_WorkTaskId");

            migrationBuilder.RenameColumn(
                name: "WorkRecordId",
                table: "SeoTaskDetails",
                newName: "WorkTaskId");

            migrationBuilder.RenameIndex(
                name: "IX_SeoTaskDetails_WorkRecordId",
                table: "SeoTaskDetails",
                newName: "IX_SeoTaskDetails_WorkTaskId");

            migrationBuilder.RenameColumn(
                name: "WorkRecordId",
                table: "OthersTaskDetails",
                newName: "WorkTaskId");

            migrationBuilder.RenameIndex(
                name: "IX_OthersTaskDetails_WorkRecordId",
                table: "OthersTaskDetails",
                newName: "IX_OthersTaskDetails_WorkTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_OthersTaskDetails_WorkTaskDetails_WorkTaskId",
                table: "OthersTaskDetails",
                column: "WorkTaskId",
                principalTable: "WorkTaskDetails",
                principalColumn: "WorkTaskId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SeoTaskDetails_WorkTaskDetails_WorkTaskId",
                table: "SeoTaskDetails",
                column: "WorkTaskId",
                principalTable: "WorkTaskDetails",
                principalColumn: "WorkTaskId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WebDeveTaskDetails_WorkTaskDetails_WorkTaskId",
                table: "WebDeveTaskDetails",
                column: "WorkTaskId",
                principalTable: "WorkTaskDetails",
                principalColumn: "WorkTaskId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OthersTaskDetails_WorkTaskDetails_WorkTaskId",
                table: "OthersTaskDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_SeoTaskDetails_WorkTaskDetails_WorkTaskId",
                table: "SeoTaskDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_WebDeveTaskDetails_WorkTaskDetails_WorkTaskId",
                table: "WebDeveTaskDetails");

            migrationBuilder.RenameColumn(
                name: "WorkTaskId",
                table: "WorkTaskDetails",
                newName: "WorkRecordId");

            migrationBuilder.RenameColumn(
                name: "WorkTaskId",
                table: "WebDeveTaskDetails",
                newName: "WorkRecordId");

            migrationBuilder.RenameIndex(
                name: "IX_WebDeveTaskDetails_WorkTaskId",
                table: "WebDeveTaskDetails",
                newName: "IX_WebDeveTaskDetails_WorkRecordId");

            migrationBuilder.RenameColumn(
                name: "WorkTaskId",
                table: "SeoTaskDetails",
                newName: "WorkRecordId");

            migrationBuilder.RenameIndex(
                name: "IX_SeoTaskDetails_WorkTaskId",
                table: "SeoTaskDetails",
                newName: "IX_SeoTaskDetails_WorkRecordId");

            migrationBuilder.RenameColumn(
                name: "WorkTaskId",
                table: "OthersTaskDetails",
                newName: "WorkRecordId");

            migrationBuilder.RenameIndex(
                name: "IX_OthersTaskDetails_WorkTaskId",
                table: "OthersTaskDetails",
                newName: "IX_OthersTaskDetails_WorkRecordId");

            migrationBuilder.AddForeignKey(
                name: "FK_OthersTaskDetails_WorkTaskDetails_WorkRecordId",
                table: "OthersTaskDetails",
                column: "WorkRecordId",
                principalTable: "WorkTaskDetails",
                principalColumn: "WorkRecordId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SeoTaskDetails_WorkTaskDetails_WorkRecordId",
                table: "SeoTaskDetails",
                column: "WorkRecordId",
                principalTable: "WorkTaskDetails",
                principalColumn: "WorkRecordId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WebDeveTaskDetails_WorkTaskDetails_WorkRecordId",
                table: "WebDeveTaskDetails",
                column: "WorkRecordId",
                principalTable: "WorkTaskDetails",
                principalColumn: "WorkRecordId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
