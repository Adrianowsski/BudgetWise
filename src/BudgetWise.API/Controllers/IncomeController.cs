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
public class IncomeController : ControllerBase
{
    private readonly AppDbContext _db;
    public IncomeController(AppDbContext db) => _db = db;

    /*------------- GET all -------------*/
    [HttpGet]
    public async Task<ActionResult<IEnumerable<IncomeDto>>> GetAll()
    {
        var list = await _db.Incomes
            .Include(i => i.IncomeType)
            .Select(i => new IncomeDto
            {
                Id            = i.Id,
                Source        = i.Source,
                Amount        = i.Amount,
                Currency      = i.Currency,
                Date          = i.Date,
                IncomeTypeId  = i.IncomeTypeId,
                IncomeTypeName= i.IncomeType.Name,
                UserId        = i.UserId
            })
            .ToListAsync();

        return Ok(list);
    }

    /*------------- GET by id -------------*/
    [HttpGet("{id}")]
    public async Task<ActionResult<IncomeDto>> Get(int id)
    {
        var i = await _db.Incomes
            .Include(x => x.IncomeType)
            .FirstOrDefaultAsync(x => x.Id == id);

        return i is null ? NotFound()
            : Ok(new IncomeDto
              {
                  Id            = i.Id,
                  Source        = i.Source,
                  Amount        = i.Amount,
                  Currency      = i.Currency,
                  Date          = i.Date,
                  IncomeTypeId  = i.IncomeTypeId,
                  IncomeTypeName= i.IncomeType.Name,
                  UserId        = i.UserId
              });
    }

    /*------------- POST -------------*/
    [HttpPost]
    public async Task<ActionResult<IncomeDto>> Create([FromBody] IncomeDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (dto.UserId == 0)     return BadRequest(new { message = "UserId missing" });

        var entity = new Income
        {
            Source       = dto.Source,
            Amount       = dto.Amount,
            Currency     = dto.Currency,
            Date         = dto.Date,
            IncomeTypeId = dto.IncomeTypeId,
            UserId       = dto.UserId
        };
        _db.Incomes.Add(entity);
        await _db.SaveChangesAsync();

        dto.Id = entity.Id;
        return CreatedAtAction(nameof(Get), new { id = dto.Id }, dto);
    }

    /*------------- PUT -------------*/
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] IncomeDto dto)
    {
        if (id != dto.Id) return BadRequest(new { message = "ID mismatch" });

        var entity = await _db.Incomes.FindAsync(id);
        if (entity is null) return NotFound();

        entity.Source       = dto.Source;
        entity.Amount       = dto.Amount;
        entity.Currency     = dto.Currency;
        entity.Date         = dto.Date;
        entity.IncomeTypeId = dto.IncomeTypeId;
        entity.UserId       = dto.UserId;

        await _db.SaveChangesAsync();
        return NoContent();
    }

    /*------------- DELETE -------------*/
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _db.Incomes.FindAsync(id);
        if (entity is null) return NotFound();

        _db.Incomes.Remove(entity);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
