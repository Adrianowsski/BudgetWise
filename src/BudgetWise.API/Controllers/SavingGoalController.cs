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
public class SavingGoalController : ControllerBase
{
    private readonly AppDbContext _context;

    public SavingGoalController(AppDbContext context) => _context = context;

    // GET /api/savinggoal
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SavingGoal>>> GetAll()
    {
        var goals = await _context.SavingGoals
            .Include(g => g.User)
            .ToListAsync();
        return Ok(goals);
    }

    // GET /api/savinggoal/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<SavingGoal>> Get(int id)
    {
        var goal = await _context.SavingGoals
            .Include(g => g.User)
            .FirstOrDefaultAsync(g => g.Id == id);

        return goal == null
            ? NotFound(new { message = "Saving goal not found." })
            : Ok(goal);
    }

    // POST /api/savinggoal
    [HttpPost]
    public async Task<ActionResult<SavingGoal>> Create([FromBody] GoalDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (dto.UserId == 0)
            return BadRequest(new { message = "User ID is required." });

        var goal = new SavingGoal
        {
            Title        = dto.Title,
            TargetAmount = dto.TargetAmount,
            Currency     = dto.Currency,
            Deadline     = dto.Deadline,
            UserId       = dto.UserId
        };

        _context.SavingGoals.Add(goal);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new { id = goal.Id }, goal);
    }

    // PUT /api/savinggoal/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] GoalDto dto)
    {
        if (id != dto.Id) return BadRequest(new { message = "ID mismatch." });

        var goal = await _context.SavingGoals.FindAsync(id);
        if (goal == null) return NotFound(new { message = "Saving goal not found." });

        goal.Title        = dto.Title;
        goal.TargetAmount = dto.TargetAmount;
        goal.Currency     = dto.Currency;
        goal.Deadline     = dto.Deadline;
        goal.UserId       = dto.UserId;

        _context.Entry(goal).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE /api/savinggoal/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var goal = await _context.SavingGoals.FindAsync(id);
        if (goal == null) return NotFound(new { message = "Saving goal not found." });

        _context.SavingGoals.Remove(goal);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
