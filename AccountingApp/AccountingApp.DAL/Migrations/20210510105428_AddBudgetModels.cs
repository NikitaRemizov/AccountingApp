using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AccountingApp.DAL.Migrations
{
    public partial class AddBudgetModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "budgetTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newid()"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_budgetTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_budgetTypes_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "budgetChange",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newid()"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BudgetTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_budgetChange", x => x.Id);
                    table.ForeignKey(
                        name: "FK_budgetChange_budgetTypes_BudgetTypeId",
                        column: x => x.BudgetTypeId,
                        principalTable: "budgetTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_budgetChange_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_budgetChange_BudgetTypeId",
                table: "budgetChange",
                column: "BudgetTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_budgetChange_UserId",
                table: "budgetChange",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_budgetTypes_UserId",
                table: "budgetTypes",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "budgetChange");

            migrationBuilder.DropTable(
                name: "budgetTypes");
        }
    }
}
