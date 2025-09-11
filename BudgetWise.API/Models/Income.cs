using System;

namespace BudgetWise.API.Models
{
    public class Income
    {
        public int Id { get; set; }
        public string Source { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "PLN"; // Nowe pole
        public DateTime Date { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public int IncomeTypeId { get; set; }
        public IncomeType IncomeType { get; set; } = null!;
    }
}
