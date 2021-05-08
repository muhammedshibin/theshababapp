using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class AddedBillPayment2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillPayment_InmateBills_BillId",
                table: "BillPayment");

            migrationBuilder.DropForeignKey(
                name: "FK_BillPayment_Inmates_InmateId",
                table: "BillPayment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BillPayment",
                table: "BillPayment");

            migrationBuilder.RenameTable(
                name: "BillPayment",
                newName: "Payments");

            migrationBuilder.RenameIndex(
                name: "IX_BillPayment_InmateId",
                table: "Payments",
                newName: "IX_Payments_InmateId");

            migrationBuilder.RenameIndex(
                name: "IX_BillPayment_BillId",
                table: "Payments",
                newName: "IX_Payments_BillId");

            migrationBuilder.AddColumn<int>(
                name: "PaymentStatus",
                table: "InmateBills",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payments",
                table: "Payments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_InmateBills_BillId",
                table: "Payments",
                column: "BillId",
                principalTable: "InmateBills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Inmates_InmateId",
                table: "Payments",
                column: "InmateId",
                principalTable: "Inmates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_InmateBills_BillId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Inmates_InmateId",
                table: "Payments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payments",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "InmateBills");

            migrationBuilder.RenameTable(
                name: "Payments",
                newName: "BillPayment");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_InmateId",
                table: "BillPayment",
                newName: "IX_BillPayment_InmateId");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_BillId",
                table: "BillPayment",
                newName: "IX_BillPayment_BillId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BillPayment",
                table: "BillPayment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BillPayment_InmateBills_BillId",
                table: "BillPayment",
                column: "BillId",
                principalTable: "InmateBills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BillPayment_Inmates_InmateId",
                table: "BillPayment",
                column: "InmateId",
                principalTable: "Inmates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
