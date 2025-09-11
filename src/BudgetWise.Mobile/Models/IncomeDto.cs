using System;
using System.ComponentModel.DataAnnotations;

namespace BudgetWise.Mobile.Models
{
    public class IncomeDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Source is required")]
        public string Source { get; set; } = string.Empty;

        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        public decimal Amount { get; set; }

        public string Currency { get; set; } = "PLN";

        [Required(ErrorMessage = "Date is required")]
        public DateTime Date { get; set; } = DateTime.Today;

        public int IncomeTypeId { get; set; }
        public string? IncomeTypeName { get; set; }

        public int UserId { get; set; }
    }
}