using Microsoft.EntityFrameworkCore.Migrations;

namespace AccountingApp.DAL.Migrations
{
    public partial class SetOnDeleteBehaviourOfBudgetChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_budgetChange_users_UserId",
                table: "budgetChange");

            migrationBuilder.AlterColumn<long>(
                name: "Amount",
                table: "budgetChange",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

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
                name: "FK_budgetChange_users_UserId",
                table: "budgetChange");

            migrationBuilder.AlterColumn<int>(
                name: "Amount",
                table: "budgetChange",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

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
