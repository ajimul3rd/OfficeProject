using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OfficeProject.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentShedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PaymentSchedule_ServiceId",
                table: "PaymentSchedule");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentSchedule_ServiceId",
                table: "PaymentSchedule",
                column: "ServiceId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PaymentSchedule_ServiceId",
                table: "PaymentSchedule");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentSchedule_ServiceId",
                table: "PaymentSchedule",
                column: "ServiceId");
        }
    }
}
