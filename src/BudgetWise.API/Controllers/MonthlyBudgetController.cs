using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
public class MonthlyBudgetController : ControllerBase
{
    private readonly AppDbContext _db;
    public MonthlyBudgetController(AppDbContext db) => _db = db;

    /* -----------  GET  /api/monthlybudget  ----------- */
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BudgetDto>>> GetAll()
    {
        await EnsureBudgetsForCurrentMonth();

        var first = FirstDayOfCurrentMonth();
        var next  = first.AddMonths(1);

        var list = await _db.MonthlyBudgets
            .Include(b => b.ExpenseCategory)
            .Where(b => b.Month >= first && b.Month < next)
            .Select(b => new BudgetDto           // <-- lambda, NIE method-group
            {
                Id                = b.Id,
                TotalAmount       = b.TotalAmount,
                Currency          = b.Currency,
                Month             = b.Month,
                ExpenseCategoryId = b.ExpenseCategoryId,
                CategoryName      = b.ExpenseCategory.Name,
                UserId            = b.UserId
            })
            .ToListAsync();                      // rozszerzenie z EF Core

        return Ok(list);
    }

    /* -------------  GET /{id}  ------------- */
    [HttpGet("{id}")]
    public async Task<ActionResult<BudgetDto>> Get(int id)
    {
        var b = await _db.MonthlyBudgets
            .Include(x => x.ExpenseCategory)
            .FirstOrDefaultAsync(x => x.Id == id);

        return b is null
            ? NotFound()
            : Ok(ToDto(b));
    }

    /* -------------  POST  ------------- */
    [HttpPost]
    public async Task<ActionResult<BudgetDto>> Create([FromBody] BudgetDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (dto.UserId == 0)     return BadRequest(new { message = "UserId missing" });

        var entity = new MonthlyBudget
        {
            TotalAmount       = dto.TotalAmount,
            Currency          = dto.Currency,
            Month             = Normalize(dto.Month),
            ExpenseCategoryId = dto.ExpenseCategoryId,
            UserId            = dto.UserId
        };

        _db.MonthlyBudgets.Add(entity);
        await _db.SaveChangesAsync();

        dto.Id   = entity.Id;
        dto.Month = entity.Month;
        return CreatedAtAction(nameof(Get), new { id = dto.Id }, dto);
    }

    /* -------------  PUT  ------------- */
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] BudgetDto dto)
    {
        if (id != dto.Id) return BadRequest(new { message = "ID mismatch" });

        var entity = await _db.MonthlyBudgets.FindAsync(id);
        if (entity is null) return NotFound();

        entity.TotalAmount       = dto.TotalAmount;
        entity.Currency          = dto.Currency;
        entity.Month             = Normalize(dto.Month);
        entity.ExpenseCategoryId = dto.ExpenseCategoryId;
        entity.UserId            = dto.UserId;

        await _db.SaveChangesAsync();
        return NoContent();
    }

    /* -------------  DELETE  ------------- */
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _db.MonthlyBudgets.FindAsync(id);
        if (entity is null) return NotFound();

        _db.MonthlyBudgets.Remove(entity);
        await _db.SaveChangesAsync();
        return NoContent();
    }

    /* =============  Helpers  ============= */
    private static DateTime FirstDayOfCurrentMonth()
    {
        var now = DateTime.UtcNow;
        return new DateTime(now.Year, now.Month, 1);
    }

    private static DateTime FirstDayOfPreviousMonth()
        => FirstDayOfCurrentMonth().AddMonths(-1);

    private static DateTime Normalize(DateTime d)
        => new DateTime(d.Year, d.Month, 1);

    private static BudgetDto ToDto(MonthlyBudget b) => new()
    {
        Id           = b.Id,
        TotalAmount  = b.TotalAmount,
        Currency     = b.Currency,
        Month        = b.Month,
        ExpenseCategoryId = b.ExpenseCategoryId,
        CategoryName      = b.ExpenseCategory.Name,
        UserId            = b.UserId
    };

    /* --------  roll-over poprzedniego miesiąca  -------- */
    private async Task EnsureBudgetsForCurrentMonth()
    {
        var cur  = FirstDayOfCurrentMonth();
        var prev = FirstDayOfPreviousMonth();

        var existing = await _db.MonthlyBudgets
            .Where(b => b.Month == cur)
            .Select(b => new { b.UserId, b.ExpenseCategoryId })
            .ToListAsync();

        var prevList = await _db.MonthlyBudgets
            .Where(b => b.Month == prev)
            .AsNoTracking()
            .ToListAsync();

        foreach (var b in prevList)
        {
            if (existing.Any(k => k.UserId == b.UserId &&
                                  k.ExpenseCategoryId == b.ExpenseCategoryId))
                continue;

            _db.MonthlyBudgets.Add(new MonthlyBudget
            {
                TotalAmount       = b.TotalAmount,
                Currency          = b.Currency,
                Month             = cur,
                UserId            = b.UserId,
                ExpenseCategoryId = b.ExpenseCategoryId
            });
        }

        if (prevList.Any())
            await _db.SaveChangesAsync();
    }
}
