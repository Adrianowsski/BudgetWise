namespace BudgetWise.API.DTOs;

public class DashboardDto
{
    public string Currency { get; set; } = "PLN";

    /* KPI cards */
    public decimal TotalIncomeMonth   { get; set; }
    public decimal TotalExpenseMonth  { get; set; }
    public decimal BalanceMonth       => TotalIncomeMonth - TotalExpenseMonth;
    public int     ActiveGoalsCount   { get; set; }

    /* pie: expenses by category (this month) */
    public Dictionary<string, decimal> PieExpByCat { get; set; } = new();

    /* bar: last 6 months */
    public List<string>   BarLabels    { get; set; } = new();          // "24-01", "24-02"…
    public List<decimal>  BarIncome    { get; set; } = new();
    public List<decimal>  BarExpense   { get; set; } = new();

    /* upcoming: label, date, link */
    public List<UpcomingItem> Upcoming { get; set; } = new();
}

public class UpcomingItem
{
    public string   Label { get; set; } = string.Empty;
    public DateTime Date  { get; set; }
    public string   Link  { get; set; } = string.Empty;
}