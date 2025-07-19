using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OfficeProject.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeyOnUserTaskMaster : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "UserTask",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserTask_UserId",
                table: "UserTask",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTask_Users_UserId",
                table: "UserTask",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTask_Users_UserId",
                table: "UserTask");

            migrationBuilder.DropIndex(
                name: "IX_UserTask_UserId",
                table: "UserTask");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserTask");
        }
    }
}
