using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BudgetWise.API.Data;
using BudgetWise.API.Models;

namespace BudgetWise.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PaymentMethodController : ControllerBase
{
    private readonly AppDbContext _context;

    public PaymentMethodController(AppDbContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PaymentMethod>>> GetAll() =>
        await _context.PaymentMethods.ToListAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<PaymentMethod>> Get(int id)
    {
        var item = await _context.PaymentMethods.FindAsync(id);
        return item == null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<PaymentMethod>> Create(PaymentMethod method)
    {
        _context.PaymentMethods.Add(method);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = method.Id }, method);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, PaymentMethod method)
    {
        if (id != method.Id) return BadRequest();
        _context.Entry(method).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var method = await _context.PaymentMethods.FindAsync(id);
        if (method == null) return NotFound();

        _context.PaymentMethods.Remove(method);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}