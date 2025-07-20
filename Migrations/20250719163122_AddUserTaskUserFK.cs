using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OfficeProject.Migrations
{
    /// <inheritdoc />
    public partial class AddUserTaskUserFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTask_Users_UserId",
                table: "UserTask");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserTask",
                newName: "UserTask_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserTask_UserId",
                table: "UserTask",
                newName: "IX_UserTask_UserTask_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTask_Users_UserTask_UserId",
                table: "UserTask",
                column: "UserTask_UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTask_Users_UserTask_UserId",
                table: "UserTask");

            migrationBuilder.RenameColumn(
                name: "UserTask_UserId",
                table: "UserTask",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserTask_UserTask_UserId",
                table: "UserTask",
                newName: "IX_UserTask_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTask_Users_UserId",
                table: "UserTask",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
