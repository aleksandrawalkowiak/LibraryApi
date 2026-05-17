namespace LibraryApi.DTOs;

public class AuthorDto
{
    public int Id { get; set; }

    public string first_name { get; set; } = string.Empty;

    public string last_name { get; set; } = string.Empty;
}