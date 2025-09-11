using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BudgetWise.API.Data;
using BudgetWise.API.Models;

namespace BudgetWise.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ExpenseCategoryController : ControllerBase
{
    private readonly AppDbContext _context;

    public ExpenseCategoryController(AppDbContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ExpenseCategory>>> GetAll() =>
        await _context.ExpenseCategories.ToListAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<ExpenseCategory>> Get(int id)
    {
        var item = await _context.ExpenseCategories.FindAsync(id);
        return item == null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<ExpenseCategory>> Create(ExpenseCategory entity)
    {
        _context.ExpenseCategories.Add(entity);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ExpenseCategory entity)
    {
        if (id != entity.Id) return BadRequest();
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.ExpenseCategories.FindAsync(id);
        if (item == null) return NotFound();
        _context.ExpenseCategories.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}