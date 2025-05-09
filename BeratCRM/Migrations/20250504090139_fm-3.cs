using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeratCRM.Migrations
{
    /// <inheritdoc />
    public partial class fm3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentHistories_Debts_DebtId",
                table: "PaymentHistories");

            migrationBuilder.DropIndex(
                name: "IX_PaymentHistories_DebtId",
                table: "PaymentHistories");

            migrationBuilder.DropColumn(
                name: "DebtId",
                table: "PaymentHistories");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentHistories_OrderId",
                table: "PaymentHistories",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentHistories_Orders_OrderId",
                table: "PaymentHistories",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentHistories_Orders_OrderId",
                table: "PaymentHistories");

            migrationBuilder.DropIndex(
                name: "IX_PaymentHistories_OrderId",
                table: "PaymentHistories");

            migrationBuilder.AddColumn<Guid>(
                name: "DebtId",
                table: "PaymentHistories",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_PaymentHistories_DebtId",
                table: "PaymentHistories",
                column: "DebtId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentHistories_Debts_DebtId",
                table: "PaymentHistories",
                column: "DebtId",
                principalTable: "Debts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
