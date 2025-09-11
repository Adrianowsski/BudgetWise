using System;
using System.ComponentModel.DataAnnotations;

namespace BudgetWise.API.DTOs
{
    public class RecurringExpenseDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; } = string.Empty;

        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        public decimal Amount { get; set; }

        public string Currency { get; set; } = "PLN";

        [Required(ErrorMessage = "Frequency is required")]
        public string Frequency { get; set; } = "Monthly";

        [Required]
        public int ExpenseCategoryId { get; set; }
        public string? CategoryName { get; set; }

        [Required]
        public int PaymentMethodId { get; set; }
        public string? PaymentMethodName { get; set; }

        public int UserId { get; set; }
    }
}