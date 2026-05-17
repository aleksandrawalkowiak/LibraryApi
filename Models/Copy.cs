namespace LibraryApi.Models;

public class Copy
{
    public int Id { get; set; }

    public bool Available { get; set; } = true;

    public int BookId { get; set; }

    public Book? Book { get; set; }
}