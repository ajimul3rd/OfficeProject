using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OfficeProject.Migrations
{
    /// <inheritdoc />
    public partial class modifyBacklinkBacklinkListEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublishTime",
                table: "BacklinkUrlList");

            migrationBuilder.DropColumn(
                name: "PublishUrl",
                table: "BacklinkUrlList");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "BacklinkUrlList");

            migrationBuilder.AddColumn<int>(
                name: "BacklinkUrlId",
                table: "BacklinkDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BacklinkUrlId",
                table: "BacklinkDetails");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "PublishTime",
                table: "BacklinkUrlList",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PublishUrl",
                table: "BacklinkUrlList",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "BacklinkUrlList",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
