using System.ComponentModel.DataAnnotations;

namespace LibraryApi.DTOs;

public class CreateBookDto
{
    [Required]
    public string Title { get; set; } = string.Empty;

    [Range(0, 3000)]
    public int Year { get; set; }

    [Required]
    public int AuthorId { get; set; }
}