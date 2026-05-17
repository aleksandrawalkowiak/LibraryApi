using LibraryApi.Data;
using LibraryApi.DTOs;
using LibraryApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Controllers;

[ApiController]
[Route("authors")]
public class AuthorsController : ControllerBase
{
    private readonly AppDbContext _context;

    public AuthorsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAll()
    {
        var authors = await _context.Authors
            .Select(a => new AuthorDto
            {
                Id = a.Id,
                first_name = a.FirstName,
                last_name = a.LastName
            })
            .ToListAsync();

        return Ok(authors);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AuthorDto>> Get(int id)
    {
        var author = await _context.Authors.FindAsync(id);

        if (author == null)
            return NotFound();

        return Ok(new AuthorDto
        {
            Id = author.Id,
            first_name = author.FirstName,
            last_name = author.LastName
        });
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateAuthorDto dto)
    {
        var author = new Author
        {
            FirstName = dto.first_name,
            LastName = dto.last_name
        };

        _context.Authors.Add(author);

        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(Get),
            new { id = author.Id },
            new AuthorDto
            {
                Id = author.Id,
                first_name = author.FirstName,
                last_name = author.LastName
            });
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, CreateAuthorDto dto)
    {
        var author = await _context.Authors.FindAsync(id);

        if (author == null)
            return NotFound();

        author.FirstName = dto.first_name;
        author.LastName = dto.last_name;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var author = await _context.Authors.FindAsync(id);

        if (author == null)
            return NotFound();

        _context.Authors.Remove(author);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}