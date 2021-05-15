using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AccountingApp.DAO.Migrations
{
    public partial class NoDeleteBudgetChangeOnBudgetTypeDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_budgetChange_budgetTypes_BudgetTypeId",
                table: "budgetChange");

            migrationBuilder.DropForeignKey(
                name: "FK_budgetChange_users_UserId",
                table: "budgetChange");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "budgetChange",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddForeignKey(
                name: "FK_budgetChange_budgetTypes_BudgetTypeId",
                table: "budgetChange",
                column: "BudgetTypeId",
                principalTable: "budgetTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_budgetChange_users_UserId",
                table: "budgetChange",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_budgetChange_budgetTypes_BudgetTypeId",
                table: "budgetChange");

            migrationBuilder.DropForeignKey(
                name: "FK_budgetChange_users_UserId",
                table: "budgetChange");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "budgetChange",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AddForeignKey(
                name: "FK_budgetChange_budgetTypes_BudgetTypeId",
                table: "budgetChange",
                column: "BudgetTypeId",
                principalTable: "budgetTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_budgetChange_users_UserId",
                table: "budgetChange",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id");
        }
    }
}
