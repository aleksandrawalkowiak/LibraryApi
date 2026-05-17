using System.ComponentModel.DataAnnotations;

namespace LibraryApi.Models;

public class Author
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string LastName { get; set; } = string.Empty;

    // Relacja 1 -> wiele
    public List<Book> Books { get; set; } = new();
}