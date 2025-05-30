using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OfficeProject.Migrations
{
    /// <inheritdoc />
    public partial class FixProjectRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_Products_ProductsId",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Projects_ProjectId",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Projects_ProjectsProjectId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_ProjectsProjectId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "ProjectsProjectId",
                table: "Services");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Products_ProductsId",
                table: "Services",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "ProductsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Projects_ProjectId",
                table: "Services",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_Products_ProductsId",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Projects_ProjectId",
                table: "Services");

            migrationBuilder.AddColumn<int>(
                name: "ProjectsProjectId",
                table: "Services",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Services_ProjectsProjectId",
                table: "Services",
                column: "ProjectsProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Products_ProductsId",
                table: "Services",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "ProductsId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Projects_ProjectId",
                table: "Services",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Projects_ProjectsProjectId",
                table: "Services",
                column: "ProjectsProjectId",
                principalTable: "Projects",
                principalColumn: "ProjectId");
        }
    }
}
