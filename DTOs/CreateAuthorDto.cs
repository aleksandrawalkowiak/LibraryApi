using System.ComponentModel.DataAnnotations;

namespace LibraryApi.DTOs;

public class CreateAuthorDto
{
    [Required]
    public string first_name { get; set; } = string.Empty;

    [Required]
    public string last_name { get; set; } = string.Empty;
}