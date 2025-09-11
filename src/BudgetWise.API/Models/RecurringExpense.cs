using System;

namespace BudgetWise.API.Models
{
    public class RecurringExpense
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "PLN"; // Nowe pole
        public string Frequency { get; set; } = string.Empty;

        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public int ExpenseCategoryId { get; set; }
        public ExpenseCategory ExpenseCategory { get; set; } = null!;
        public int PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; } = null!;
    }
}
