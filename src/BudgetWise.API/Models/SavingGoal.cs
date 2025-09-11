using System;

namespace BudgetWise.API.Models
{
    public class SavingGoal
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal TargetAmount { get; set; }
        public string Currency { get; set; } = "PLN"; // Nowe pole
        public DateTime Deadline { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
