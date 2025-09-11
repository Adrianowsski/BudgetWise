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
public class ExpenseController : ControllerBase
{
    private readonly AppDbContext _db;
    public ExpenseController(AppDbContext db) => _db = db;

    /*--------------- GET all ---------------*/
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ExpenseDto>>> GetAll()
    {
        var list = await _db.Expenses
            .Include(e => e.ExpenseCategory)
            .Include(e => e.PaymentMethod)
            .Select(e => new ExpenseDto
            {
                Id                = e.Id,
                Description       = e.Description,
                Amount            = e.Amount,
                Currency          = e.Currency,
                Date              = e.Date,
                ExpenseCategoryId = e.ExpenseCategoryId,
                CategoryName      = e.ExpenseCategory.Name,
                PaymentMethodId   = e.PaymentMethodId,
                PaymentMethodName = e.PaymentMethod.Name,
                UserId            = e.UserId
            })
            .ToListAsync();

        return Ok(list);
    }

    /*--------------- GET by id ---------------*/
    [HttpGet("{id}")]
    public async Task<ActionResult<ExpenseDto>> Get(int id)
    {
        var e = await _db.Expenses
            .Include(x => x.ExpenseCategory)
            .Include(x => x.PaymentMethod)
            .FirstOrDefaultAsync(x => x.Id == id);

        return e is null ? NotFound()
            : Ok(new ExpenseDto
            {
                Id                = e.Id,
                Description       = e.Description,
                Amount            = e.Amount,
                Currency          = e.Currency,
                Date              = e.Date,
                ExpenseCategoryId = e.ExpenseCategoryId,
                CategoryName      = e.ExpenseCategory.Name,
                PaymentMethodId   = e.PaymentMethodId,
                PaymentMethodName = e.PaymentMethod.Name,
                UserId            = e.UserId
            });
    }

    /*--------------- POST ---------------*/
    [HttpPost]
    public async Task<ActionResult<ExpenseDto>> Create([FromBody] ExpenseDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (dto.UserId == 0)     return BadRequest(new { message = "UserId missing" });

        var entity = new Expense
        {
            Description       = dto.Description,
            Amount            = dto.Amount,
            Currency          = dto.Currency,
            Date              = dto.Date,
            ExpenseCategoryId = dto.ExpenseCategoryId,
            PaymentMethodId   = dto.PaymentMethodId,
            UserId            = dto.UserId
        };

        _db.Expenses.Add(entity);
        await _db.SaveChangesAsync();

        dto.Id = entity.Id;
        return CreatedAtAction(nameof(Get), new { id = dto.Id }, dto);
    }

    /*--------------- PUT ---------------*/
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ExpenseDto dto)
    {
        if (id != dto.Id) return BadRequest(new { message = "ID mismatch" });

        var entity = await _db.Expenses.FindAsync(id);
        if (entity is null) return NotFound();

        entity.Description       = dto.Description;
        entity.Amount            = dto.Amount;
        entity.Currency          = dto.Currency;
        entity.Date              = dto.Date;
        entity.ExpenseCategoryId = dto.ExpenseCategoryId;
        entity.PaymentMethodId   = dto.PaymentMethodId;
        entity.UserId            = dto.UserId;

        await _db.SaveChangesAsync();
        return NoContent();
    }

    /*--------------- DELETE ---------------*/
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _db.Expenses.FindAsync(id);
        if (entity is null) return NotFound();

        _db.Expenses.Remove(entity);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
