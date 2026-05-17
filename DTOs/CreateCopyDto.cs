using System.ComponentModel.DataAnnotations;

namespace LibraryApi.DTOs;

public class CreateCopyDto
{
    [Required]
    public int BookId { get; set; }

    public bool Available { get; set; } = true;
}