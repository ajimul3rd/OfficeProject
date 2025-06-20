using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OfficeProject.Migrations
{
    /// <inheritdoc />
    public partial class RemovedProjectType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProjectType",
                table: "Projects");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProjectType",
                table: "Projects",
                type: "nvarchar(50)",
                nullable: true);
        }
    }
}
