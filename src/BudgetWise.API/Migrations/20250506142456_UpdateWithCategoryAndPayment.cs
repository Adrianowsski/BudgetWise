using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgetWise.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateWithCategoryAndPayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_ExpenseCategories_ExpenseCategoryId",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Incomes_IncomeTypes_IncomeTypeId",
                table: "Incomes");

            migrationBuilder.AddColumn<int>(
                name: "ExpenseCategoryId",
                table: "RecurringExpenses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PaymentMethodId",
                table: "RecurringExpenses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ExpenseCategoryId",
                table: "MonthlyBudgets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "IncomeTypeId",
                table: "Incomes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ExpenseCategoryId",
                table: "Expenses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentMethodId",
                table: "Expenses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_RecurringExpenses_ExpenseCategoryId",
                table: "RecurringExpenses",
                column: "ExpenseCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_RecurringExpenses_PaymentMethodId",
                table: "RecurringExpenses",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_MonthlyBudgets_ExpenseCategoryId",
                table: "MonthlyBudgets",
                column: "ExpenseCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_PaymentMethodId",
                table: "Expenses",
                column: "PaymentMethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_ExpenseCategories_ExpenseCategoryId",
                table: "Expenses",
                column: "ExpenseCategoryId",
                principalTable: "ExpenseCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_PaymentMethods_PaymentMethodId",
                table: "Expenses",
                column: "PaymentMethodId",
                principalTable: "PaymentMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Incomes_IncomeTypes_IncomeTypeId",
                table: "Incomes",
                column: "IncomeTypeId",
                principalTable: "IncomeTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MonthlyBudgets_ExpenseCategories_ExpenseCategoryId",
                table: "MonthlyBudgets",
                column: "ExpenseCategoryId",
                principalTable: "ExpenseCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecurringExpenses_ExpenseCategories_ExpenseCategoryId",
                table: "RecurringExpenses",
                column: "ExpenseCategoryId",
                principalTable: "ExpenseCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecurringExpenses_PaymentMethods_PaymentMethodId",
                table: "RecurringExpenses",
                column: "PaymentMethodId",
                principalTable: "PaymentMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_ExpenseCategories_ExpenseCategoryId",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_PaymentMethods_PaymentMethodId",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Incomes_IncomeTypes_IncomeTypeId",
                table: "Incomes");

            migrationBuilder.DropForeignKey(
                name: "FK_MonthlyBudgets_ExpenseCategories_ExpenseCategoryId",
                table: "MonthlyBudgets");

            migrationBuilder.DropForeignKey(
                name: "FK_RecurringExpenses_ExpenseCategories_ExpenseCategoryId",
                table: "RecurringExpenses");

            migrationBuilder.DropForeignKey(
                name: "FK_RecurringExpenses_PaymentMethods_PaymentMethodId",
                table: "RecurringExpenses");

            migrationBuilder.DropIndex(
                name: "IX_RecurringExpenses_ExpenseCategoryId",
                table: "RecurringExpenses");

            migrationBuilder.DropIndex(
                name: "IX_RecurringExpenses_PaymentMethodId",
                table: "RecurringExpenses");

            migrationBuilder.DropIndex(
                name: "IX_MonthlyBudgets_ExpenseCategoryId",
                table: "MonthlyBudgets");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_PaymentMethodId",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "ExpenseCategoryId",
                table: "RecurringExpenses");

            migrationBuilder.DropColumn(
                name: "PaymentMethodId",
                table: "RecurringExpenses");

            migrationBuilder.DropColumn(
                name: "ExpenseCategoryId",
                table: "MonthlyBudgets");

            migrationBuilder.DropColumn(
                name: "PaymentMethodId",
                table: "Expenses");

            migrationBuilder.AlterColumn<int>(
                name: "IncomeTypeId",
                table: "Incomes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ExpenseCategoryId",
                table: "Expenses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_ExpenseCategories_ExpenseCategoryId",
                table: "Expenses",
                column: "ExpenseCategoryId",
                principalTable: "ExpenseCategories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Incomes_IncomeTypes_IncomeTypeId",
                table: "Incomes",
                column: "IncomeTypeId",
                principalTable: "IncomeTypes",
                principalColumn: "Id");
        }
    }
}
