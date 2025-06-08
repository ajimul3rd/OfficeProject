using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OfficeProject.Migrations
{
    /// <inheritdoc />
    public partial class updateentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "workingActivityName",
                table: "UserWorkingActivityList",
                newName: "MasterActivityName");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "UserWorkingActivityList",
                newName: "MasterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MasterActivityName",
                table: "UserWorkingActivityList",
                newName: "workingActivityName");

            migrationBuilder.RenameColumn(
                name: "MasterId",
                table: "UserWorkingActivityList",
                newName: "Id");
        }
    }
}
