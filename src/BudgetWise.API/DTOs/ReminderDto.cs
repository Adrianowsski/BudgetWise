namespace BudgetWise.API.DTOs;

public class ReminderDto
{
    public int Id { get; set; }
    public string Message { get; set; }
    public DateTime RemindAt { get; set; }
    public int UserId { get; set; } // Dodaj, jeśli brakowało
}
