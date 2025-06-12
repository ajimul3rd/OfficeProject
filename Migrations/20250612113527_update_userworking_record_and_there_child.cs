using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OfficeProject.Migrations
{
    /// <inheritdoc />
    public partial class update_userworking_record_and_there_child : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkRecordsSeoDetails_WorkRecords_WorkRecordId",
                table: "WorkRecordsSeoDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkRecordsSeoDetails",
                table: "WorkRecordsSeoDetails");

            migrationBuilder.RenameTable(
                name: "WorkRecordsSeoDetails",
                newName: "SeoTaskDetails");

            migrationBuilder.RenameIndex(
                name: "IX_WorkRecordsSeoDetails_WorkRecordId",
                table: "SeoTaskDetails",
                newName: "IX_SeoTaskDetails_WorkRecordId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SeoTaskDetails",
                table: "SeoTaskDetails",
                column: "SeoTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_SeoTaskDetails_WorkRecords_WorkRecordId",
                table: "SeoTaskDetails",
                column: "WorkRecordId",
                principalTable: "WorkRecords",
                principalColumn: "WorkRecordId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SeoTaskDetails_WorkRecords_WorkRecordId",
                table: "SeoTaskDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SeoTaskDetails",
                table: "SeoTaskDetails");

            migrationBuilder.RenameTable(
                name: "SeoTaskDetails",
                newName: "WorkRecordsSeoDetails");

            migrationBuilder.RenameIndex(
                name: "IX_SeoTaskDetails_WorkRecordId",
                table: "WorkRecordsSeoDetails",
                newName: "IX_WorkRecordsSeoDetails_WorkRecordId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkRecordsSeoDetails",
                table: "WorkRecordsSeoDetails",
                column: "SeoTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkRecordsSeoDetails_WorkRecords_WorkRecordId",
                table: "WorkRecordsSeoDetails",
                column: "WorkRecordId",
                principalTable: "WorkRecords",
                principalColumn: "WorkRecordId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
