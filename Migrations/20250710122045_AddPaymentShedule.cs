//using Microsoft.EntityFrameworkCore.Migrations;

//#nullable disable

//namespace OfficeProject.Migrations
//{
//    /// <inheritdoc />
//    public partial class AddPaymentShedule : Migration
//    {
//        /// <inheritdoc />
//        protected override void Up(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.DropIndex(
//                name: "IX_PaymentSchedule_ServiceId",
//                table: "PaymentSchedule");

//            migrationBuilder.CreateIndex(
//                name: "IX_PaymentSchedule_ServiceId",
//                table: "PaymentSchedule",
//                column: "ServiceId",
//                unique: true);
//        }

//        /// <inheritdoc />
//        protected override void Down(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.DropIndex(
//                name: "IX_PaymentSchedule_ServiceId",
//                table: "PaymentSchedule");

//            migrationBuilder.CreateIndex(
//                name: "IX_PaymentSchedule_ServiceId",
//                table: "PaymentSchedule",
//                column: "ServiceId");
//        }
//    }
//}
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
            migrationBuilder.Sql(@"
                IF EXISTS (
                    SELECT 1 FROM sys.indexes 
                    WHERE name = 'IX_PaymentSchedule_ServiceId' 
                      AND object_id = OBJECT_ID('dbo.PaymentSchedule')
                )
                BEGIN
                    RAISERROR ('Dropping existing IX_PaymentSchedule_ServiceId index.', 10, 1) WITH NOWAIT;
                    DROP INDEX [IX_PaymentSchedule_ServiceId] ON [PaymentSchedule];
                END
            ");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentSchedule_ServiceId",
                table: "PaymentSchedule",
                column: "ServiceId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF EXISTS (
                    SELECT 1 FROM sys.indexes 
                    WHERE name = 'IX_PaymentSchedule_ServiceId' 
                      AND object_id = OBJECT_ID('dbo.PaymentSchedule')
                )
                BEGIN
                    RAISERROR ('Dropping existing IX_PaymentSchedule_ServiceId index.', 10, 1) WITH NOWAIT;
                    DROP INDEX [IX_PaymentSchedule_ServiceId] ON [PaymentSchedule];
                END
            ");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentSchedule_ServiceId",
                table: "PaymentSchedule",
                column: "ServiceId");
        }
    }
}
