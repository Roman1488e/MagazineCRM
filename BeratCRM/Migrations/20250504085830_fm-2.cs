using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeratCRM.Migrations
{
    /// <inheritdoc />
    public partial class fm2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Debts_PaymentHistories_PaymentHistoryId",
                table: "Debts");

            migrationBuilder.DropIndex(
                name: "IX_Debts_PaymentHistoryId",
                table: "Debts");

            migrationBuilder.DropColumn(
                name: "PaymentHistoryId",
                table: "Debts");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentHistoryId",
                table: "Debts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Debts_PaymentHistoryId",
                table: "Debts",
                column: "PaymentHistoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Debts_PaymentHistories_PaymentHistoryId",
                table: "Debts",
                column: "PaymentHistoryId",
                principalTable: "PaymentHistories",
                principalColumn: "PaymentHistoryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
