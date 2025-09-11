using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BudgetWise.API.Data;
using BudgetWise.API.Models;

namespace BudgetWise.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class IncomeTypeController : ControllerBase
{
    private readonly AppDbContext _context;

    public IncomeTypeController(AppDbContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<IncomeType>>> GetAll() =>
        await _context.IncomeTypes.ToListAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<IncomeType>> Get(int id)
    {
        var item = await _context.IncomeTypes.FindAsync(id);
        return item == null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<IncomeType>> Create(IncomeType entity)
    {
        _context.IncomeTypes.Add(entity);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, IncomeType entity)
    {
        if (id != entity.Id) return BadRequest();
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.IncomeTypes.FindAsync(id);
        if (item == null) return NotFound();
        _context.IncomeTypes.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}