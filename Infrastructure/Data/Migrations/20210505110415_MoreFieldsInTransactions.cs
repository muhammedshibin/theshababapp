using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class MoreFieldsInTransactions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaidToId",
                table: "Transactions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_PaidToId",
                table: "Transactions",
                column: "PaidToId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Vendors_PaidToId",
                table: "Transactions",
                column: "PaidToId",
                principalTable: "Vendors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Vendors_PaidToId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_PaidToId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "PaidToId",
                table: "Transactions");
        }
    }
}
