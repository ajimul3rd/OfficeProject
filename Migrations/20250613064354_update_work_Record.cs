using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OfficeProject.Migrations
{
    /// <inheritdoc />
    public partial class update_work_Record : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServiceName",
                table: "WorkRecords");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ServiceName",
                table: "WorkRecords",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
