using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OfficeProject.Migrations
{
    /// <inheritdoc />
    public partial class AddeMoredFieldOnWorkTaskDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BacklinkURL",
                table: "WorkTaskDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ClasifiedURL",
                table: "WorkTaskDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SocialSharingURL",
                table: "WorkTaskDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BacklinkURL",
                table: "WorkTaskDetails");

            migrationBuilder.DropColumn(
                name: "ClasifiedURL",
                table: "WorkTaskDetails");

            migrationBuilder.DropColumn(
                name: "SocialSharingURL",
                table: "WorkTaskDetails");
        }
    }
}
