using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OfficeProject.Migrations
{
    /// <inheritdoc />
    public partial class AddedFieldOnWorkTaskDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Backlink",
                table: "WorkTaskDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Clasified",
                table: "WorkTaskDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SocialSharing",
                table: "WorkTaskDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Backlink",
                table: "WorkTaskDetails");

            migrationBuilder.DropColumn(
                name: "Clasified",
                table: "WorkTaskDetails");

            migrationBuilder.DropColumn(
                name: "SocialSharing",
                table: "WorkTaskDetails");
        }
    }
}
