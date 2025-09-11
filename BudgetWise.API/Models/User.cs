using System.Collections.Generic;

namespace BudgetWise.API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string PasswordSalt { get; set; } = string.Empty;

        public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
        public ICollection<Income> Incomes { get; set; } = new List<Income>();
        public ICollection<MonthlyBudget> Budgets { get; set; } = new List<MonthlyBudget>();
    }
}