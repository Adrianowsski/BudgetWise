namespace BudgetWise.Mobile.Models;

public class PaymentMethodDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Provider { get; set; } = string.Empty;
    
}