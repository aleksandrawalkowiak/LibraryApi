using System.ComponentModel.DataAnnotations;

namespace LibraryApi.Models;

public class Book
{
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [Range(0, 3000)]
    public int Year { get; set; }

    // FK
    public int AuthorId { get; set; }

    // Nawigacja
    public Author? Author { get; set; }

    public List<Copy> Copies { get; set; } = new();
}