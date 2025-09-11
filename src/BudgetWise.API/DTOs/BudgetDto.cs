using System;
using System.ComponentModel.DataAnnotations;

namespace BudgetWise.API.DTOs
{
    public class BudgetDto
    {
        public int Id { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Total must be greater than 0")]
        public decimal TotalAmount { get; set; }

        public string Currency { get; set; } = "PLN";

        [Required(ErrorMessage = "Month is required")]
        public DateTime Month { get; set; } = DateTime.Today;

        [Required]
        public int ExpenseCategoryId { get; set; }
        public string? CategoryName { get; set; }

        public int UserId { get; set; }
    }
}