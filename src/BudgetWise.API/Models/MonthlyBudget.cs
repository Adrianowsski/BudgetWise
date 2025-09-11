using System;

namespace BudgetWise.API.Models
{
    public class MonthlyBudget
    {
        public int Id { get; set; }
        public decimal TotalAmount { get; set; }
        public string Currency { get; set; } = "PLN"; // Nowe pole
        public DateTime Month { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public int ExpenseCategoryId { get; set; }
        public ExpenseCategory ExpenseCategory { get; set; } = null!;
    }
}
