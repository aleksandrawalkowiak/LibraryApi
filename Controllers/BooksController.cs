using LibraryApi.Data;
using LibraryApi.DTOs;
using LibraryApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Controllers;

[ApiController]
[Route("books")]
public class BooksController : ControllerBase
{
    private readonly AppDbContext _context;

    public BooksController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookDto>>> GetAll([FromQuery] int? authorId)
    {
        var query = _context.Books
            .Include(b => b.Author)
            .AsQueryable();

        if (authorId.HasValue)
        {
            query = query.Where(b => b.AuthorId == authorId);
        }

        var books = await query
            .Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                Year = b.Year,
                Author = new AuthorDto
                {
                    Id = b.Author!.Id,
                    first_name = b.Author.FirstName,
                    last_name = b.Author.LastName
                }
            })
            .ToListAsync();

        return Ok(books);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BookDto>> Get(int id)
    {
        var book = await _context.Books
            .Include(b => b.Author)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (book == null)
            return NotFound();

        return Ok(new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            Year = book.Year,
            Author = new AuthorDto
            {
                Id = book.Author!.Id,
                first_name = book.Author.FirstName,
                last_name = book.Author.LastName
            }
        });
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateBookDto dto)
    {
        var authorExists = await _context.Authors
            .AnyAsync(a => a.Id == dto.AuthorId);

        if (!authorExists)
            return BadRequest("Author does not exist");

        var book = new Book
        {
            Title = dto.Title,
            Year = dto.Year,
            AuthorId = dto.AuthorId
        };

        _context.Books.Add(book);

        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(Get),
            new { id = book.Id },
            book);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, CreateBookDto dto)
    {
        var book = await _context.Books.FindAsync(id);

        if (book == null)
            return NotFound();

        book.Title = dto.Title;
        book.Year = dto.Year;
        book.AuthorId = dto.AuthorId;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var book = await _context.Books.FindAsync(id);

        if (book == null)
            return NotFound();

        _context.Books.Remove(book);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}