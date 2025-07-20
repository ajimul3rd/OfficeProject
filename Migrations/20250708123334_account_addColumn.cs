using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OfficeProject.Migrations
{
    /// <inheritdoc />
    public partial class account_addColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Balance",
                table: "Accounts",
                newName: "OpeningBalance");

            migrationBuilder.AddColumn<decimal>(
                name: "ClosedBalance",
                table: "Accounts",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "ClosedDate",
                table: "Accounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsClosed",
                table: "Accounts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClosedBalance",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "ClosedDate",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "IsClosed",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "OpeningBalance",
                table: "Accounts",
                newName: "Balance");
        }
    }
}
