using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgetWise.API.Migrations
{
    /// <inheritdoc />
    public partial class AddCurrencyToModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "SavingGoals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "RecurringExpenses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "MonthlyBudgets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "Incomes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "Expenses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                table: "SavingGoals");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "RecurringExpenses");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "MonthlyBudgets");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Incomes");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Expenses");
        }
    }
}
