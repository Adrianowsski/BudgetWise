using System;
using System.ComponentModel.DataAnnotations;

namespace BudgetWise.API.DTOs
{
    public class ExpenseDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; } = string.Empty;

        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        public decimal Amount { get; set; }

        public string Currency { get; set; } = "PLN";

        [Required] public DateTime Date { get; set; } = DateTime.Today;

        [Required] public int ExpenseCategoryId { get; set; }
        public string? CategoryName { get; set; }

        [Required] public int PaymentMethodId { get; set; }
        public string? PaymentMethodName { get; set; }

        public int UserId { get; set; }
    }
}