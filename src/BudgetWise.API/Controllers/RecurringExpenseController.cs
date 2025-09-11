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
public class RecurringExpenseController : ControllerBase
{
    private readonly AppDbContext _context;
    public RecurringExpenseController(AppDbContext context) => _context = context;

    // GET /api/recurringexpense
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RecurringExpenseDto>>> GetAll()
    {
        var data = await _context.RecurringExpenses
            .Include(e => e.ExpenseCategory)
            .Include(e => e.PaymentMethod)
            .ToListAsync();

        var dto = data.Select(e => new RecurringExpenseDto
        {
            Id                = e.Id,
            Title             = e.Title,
            Amount            = e.Amount,
            Currency          = e.Currency,
            Frequency         = e.Frequency,
            ExpenseCategoryId = e.ExpenseCategoryId,
            CategoryName      = e.ExpenseCategory.Name,
            PaymentMethodId   = e.PaymentMethodId,
            PaymentMethodName = e.PaymentMethod.Name,
            UserId            = e.UserId
        });

        return Ok(dto);
    }

    // GET /api/recurringexpense/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<RecurringExpenseDto>> Get(int id)
    {
        var e = await _context.RecurringExpenses
            .Include(x => x.ExpenseCategory)
            .Include(x => x.PaymentMethod)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (e == null) return NotFound();

        return Ok(new RecurringExpenseDto
        {
            Id                = e.Id,
            Title             = e.Title,
            Amount            = e.Amount,
            Currency          = e.Currency,
            Frequency         = e.Frequency,
            ExpenseCategoryId = e.ExpenseCategoryId,
            CategoryName      = e.ExpenseCategory.Name,
            PaymentMethodId   = e.PaymentMethodId,
            PaymentMethodName = e.PaymentMethod.Name,
            UserId            = e.UserId
        });
    }

    // POST /api/recurringexpense
    [HttpPost]
    public async Task<ActionResult<RecurringExpenseDto>> Create([FromBody] RecurringExpenseDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (dto.UserId == 0)     return BadRequest(new { message = "User ID is required." });

        var entity = new RecurringExpense
        {
            Title             = dto.Title,
            Amount            = dto.Amount,
            Currency          = dto.Currency,
            Frequency         = dto.Frequency,
            ExpenseCategoryId = dto.ExpenseCategoryId,
            PaymentMethodId   = dto.PaymentMethodId,
            UserId            = dto.UserId
        };

        _context.RecurringExpenses.Add(entity);
        await _context.SaveChangesAsync();

        dto.Id = entity.Id;
        return CreatedAtAction(nameof(Get), new { id = dto.Id }, dto);
    }

    // PUT /api/recurringexpense/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] RecurringExpenseDto dto)
    {
        if (id != dto.Id) return BadRequest(new { message = "ID mismatch." });

        var entity = await _context.RecurringExpenses.FindAsync(id);
        if (entity == null) return NotFound();

        entity.Title             = dto.Title;
        entity.Amount            = dto.Amount;
        entity.Currency          = dto.Currency;
        entity.Frequency         = dto.Frequency;
        entity.ExpenseCategoryId = dto.ExpenseCategoryId;
        entity.PaymentMethodId   = dto.PaymentMethodId;
        entity.UserId            = dto.UserId;

        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE /api/recurringexpense/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _context.RecurringExpenses.FindAsync(id);
        if (entity == null) return NotFound();

        _context.RecurringExpenses.Remove(entity);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
