using Microsoft.EntityFrameworkCore.Migrations;

namespace DAO.Migrations
{
    public partial class ChangeOnDeleteBehaviour : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_budgetChange_budgetTypes_BudgetTypeId",
                table: "budgetChange");

            migrationBuilder.DropForeignKey(
                name: "FK_budgetChange_users_UserId",
                table: "budgetChange");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_budgetChange_budgetTypes_BudgetTypeId",
                table: "budgetChange");

            migrationBuilder.DropForeignKey(
                name: "FK_budgetChange_users_UserId",
                table: "budgetChange");

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
    }
}
