using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAO.Migrations
{
    public partial class ChangeOnDeleteBehaviourToSetNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_budgetChange_budgetTypes_BudgetTypeId",
                table: "budgetChange");

            migrationBuilder.DropForeignKey(
                name: "FK_budgetTypes_users_UserId",
                table: "budgetTypes");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "budgetTypes",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "newid()");

            migrationBuilder.AddForeignKey(
                name: "FK_budgetChange_budgetTypes_BudgetTypeId",
                table: "budgetChange",
                column: "BudgetTypeId",
                principalTable: "budgetTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_budgetTypes_users_UserId",
                table: "budgetTypes",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_budgetChange_budgetTypes_BudgetTypeId",
                table: "budgetChange");

            migrationBuilder.DropForeignKey(
                name: "FK_budgetTypes_users_UserId",
                table: "budgetTypes");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "budgetTypes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "newid()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_budgetChange_budgetTypes_BudgetTypeId",
                table: "budgetChange",
                column: "BudgetTypeId",
                principalTable: "budgetTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_budgetTypes_users_UserId",
                table: "budgetTypes",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
