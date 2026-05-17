using LibraryApi.Data;
using LibraryApi.DTOs;
using LibraryApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Controllers;

[ApiController]
[Route("copies")]
public class CopiesController : ControllerBase
{
    private readonly AppDbContext _context;

    public CopiesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CopyDto>>> GetAll()
    {
        var copies = await _context.Copies
            .Select(c => new CopyDto
            {
                Id = c.Id,
                Available = c.Available,
                BookId = c.BookId
            })
            .ToListAsync();

        return Ok(copies);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CopyDto>> Get(int id)
    {
        var copy = await _context.Copies.FindAsync(id);

        if (copy == null)
            return NotFound();

        return Ok(new CopyDto
        {
            Id = copy.Id,
            Available = copy.Available,
            BookId = copy.BookId
        });
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateCopyDto dto)
    {
        var bookExists = await _context.Books
            .AnyAsync(b => b.Id == dto.BookId);

        if (!bookExists)
            return BadRequest("Book does not exist");

        var copy = new Copy
        {
            BookId = dto.BookId,
            Available = dto.Available
        };

        _context.Copies.Add(copy);

        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(Get),
            new { id = copy.Id },
            copy);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, CreateCopyDto dto)
    {
        var copy = await _context.Copies.FindAsync(id);

        if (copy == null)
            return NotFound();

        copy.BookId = dto.BookId;
        copy.Available = dto.Available;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var copy = await _context.Copies.FindAsync(id);

        if (copy == null)
            return NotFound();

        _context.Copies.Remove(copy);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}