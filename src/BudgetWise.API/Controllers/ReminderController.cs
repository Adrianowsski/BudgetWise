using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BudgetWise.API.Data;
using BudgetWise.API.DTOs;
using BudgetWise.API.Models;

namespace BudgetWise.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ReminderController : ControllerBase
{
    private readonly AppDbContext _context;

    public ReminderController(AppDbContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Reminder>>> GetAll()
    {
        var reminders = await _context.Reminders
            .Include(r => r.User)
            .ToListAsync();
        return Ok(reminders);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Reminder>> Get(int id)
    {
        var reminder = await _context.Reminders
            .Include(r => r.User)
            .FirstOrDefaultAsync(r => r.Id == id);

        if (reminder == null)
            return NotFound(new { message = "Reminder not found." });

        return Ok(reminder);
    }

    [HttpPost]
    public async Task<ActionResult<Reminder>> Create([FromBody] ReminderDto reminderDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Dodaj mapowanie z ReminderDto na Reminder
        var reminder = new Reminder
        {
            Message = reminderDto.Message,
            RemindAt = reminderDto.RemindAt,
            UserId = reminderDto.UserId // Upewnij się, że ID użytkownika jest ustawione
        };

        _context.Reminders.Add(reminder);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = reminder.Id }, reminder);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ReminderDto reminderDto)
    {
        if (id != reminderDto.Id) 
            return BadRequest(new { message = "ID mismatch." });

        var existingReminder = await _context.Reminders.FindAsync(id);
        if (existingReminder == null) 
            return NotFound(new { message = "Reminder not found." });

        existingReminder.Message = reminderDto.Message;
        existingReminder.RemindAt = reminderDto.RemindAt;
        existingReminder.UserId = reminderDto.UserId;

        try
        {
            _context.Entry(existingReminder).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = $"Error updating reminder: {ex.Message}" });
        }
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var reminder = await _context.Reminders.FindAsync(id);
        if (reminder == null)
            return NotFound(new { message = "Reminder not found." });

        _context.Reminders.Remove(reminder);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
