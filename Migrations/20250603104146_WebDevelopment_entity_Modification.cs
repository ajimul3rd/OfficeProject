using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OfficeProject.Migrations
{
    /// <inheritdoc />
    public partial class WebDevelopment_entity_Modification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WebDevelopment_Services_ProjectId",
                table: "WebDevelopment");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "WebDevelopment",
                newName: "ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_WebDevelopment_ProjectId",
                table: "WebDevelopment",
                newName: "IX_WebDevelopment_ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_WebDevelopment_Services_ServiceId",
                table: "WebDevelopment",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "ServiceId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WebDevelopment_Services_ServiceId",
                table: "WebDevelopment");

            migrationBuilder.RenameColumn(
                name: "ServiceId",
                table: "WebDevelopment",
                newName: "ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_WebDevelopment_ServiceId",
                table: "WebDevelopment",
                newName: "IX_WebDevelopment_ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_WebDevelopment_Services_ProjectId",
                table: "WebDevelopment",
                column: "ProjectId",
                principalTable: "Services",
                principalColumn: "ServiceId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
