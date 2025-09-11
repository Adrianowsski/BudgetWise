using System.ComponentModel.DataAnnotations;

namespace BudgetWise.Mobile.Models
{
    public class ReminderDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Message is required")]
        public string Message { get; set; } = string.Empty;

        [Required(ErrorMessage = "RemindAt date is required")]
        public DateTime RemindAt { get; set; }
        public int UserId { get; set; }
    }
}