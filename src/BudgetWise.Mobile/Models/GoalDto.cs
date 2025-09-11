using System;
using System.ComponentModel.DataAnnotations;

namespace BudgetWise.Mobile.Models
{
    public class GoalDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; } = string.Empty;

        [Range(0.01, double.MaxValue, ErrorMessage = "Target must be greater than 0")]
        public decimal TargetAmount { get; set; }

        public string Currency { get; set; } = "PLN";

        [Required(ErrorMessage = "Deadline is required")]
        public DateTime Deadline { get; set; } = DateTime.Today;

        public int UserId { get; set; }
    }
}