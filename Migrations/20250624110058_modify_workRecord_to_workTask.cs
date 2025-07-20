using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OfficeProject.Migrations
{
    /// <inheritdoc />
    public partial class modify_workRecord_to_workTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OthersTaskDetails_WorkRecords_WorkRecordId",
                table: "OthersTaskDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_SeoTaskDetails_WorkRecords_WorkRecordId",
                table: "SeoTaskDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_WebDeveTaskDetails_WorkRecords_WorkRecordId",
                table: "WebDeveTaskDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkRecords_Services_ServiceId",
                table: "WorkRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkRecords_Users_Work_UserId",
                table: "WorkRecords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkRecords",
                table: "WorkRecords");

            migrationBuilder.RenameTable(
                name: "WorkRecords",
                newName: "WorkTaskDetails");

            migrationBuilder.RenameIndex(
                name: "IX_WorkRecords_Work_UserId",
                table: "WorkTaskDetails",
                newName: "IX_WorkTaskDetails_Work_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkRecords_ServiceId",
                table: "WorkTaskDetails",
                newName: "IX_WorkTaskDetails_ServiceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkTaskDetails",
                table: "WorkTaskDetails",
                column: "WorkRecordId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_WorkTaskDetails_Services_ServiceId",
                table: "WorkTaskDetails",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "ServiceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkTaskDetails_Users_Work_UserId",
                table: "WorkTaskDetails",
                column: "Work_UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropForeignKey(
                name: "FK_WorkTaskDetails_Services_ServiceId",
                table: "WorkTaskDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkTaskDetails_Users_Work_UserId",
                table: "WorkTaskDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkTaskDetails",
                table: "WorkTaskDetails");

            migrationBuilder.RenameTable(
                name: "WorkTaskDetails",
                newName: "WorkRecords");

            migrationBuilder.RenameIndex(
                name: "IX_WorkTaskDetails_Work_UserId",
                table: "WorkRecords",
                newName: "IX_WorkRecords_Work_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkTaskDetails_ServiceId",
                table: "WorkRecords",
                newName: "IX_WorkRecords_ServiceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkRecords",
                table: "WorkRecords",
                column: "WorkRecordId");

            migrationBuilder.AddForeignKey(
                name: "FK_OthersTaskDetails_WorkRecords_WorkRecordId",
                table: "OthersTaskDetails",
                column: "WorkRecordId",
                principalTable: "WorkRecords",
                principalColumn: "WorkRecordId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SeoTaskDetails_WorkRecords_WorkRecordId",
                table: "SeoTaskDetails",
                column: "WorkRecordId",
                principalTable: "WorkRecords",
                principalColumn: "WorkRecordId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WebDeveTaskDetails_WorkRecords_WorkRecordId",
                table: "WebDeveTaskDetails",
                column: "WorkRecordId",
                principalTable: "WorkRecords",
                principalColumn: "WorkRecordId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkRecords_Services_ServiceId",
                table: "WorkRecords",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "ServiceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkRecords_Users_Work_UserId",
                table: "WorkRecords",
                column: "Work_UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
