using System.Collections.Generic;

namespace BudgetWise.API.Models
{
    public class IncomeType
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public ICollection<Income> Incomes { get; set; } = new List<Income>();
    }
}