using System;

namespace BudgetWise.API.Models
{
    public class Reminder
    {
        public int Id { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime RemindAt { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}