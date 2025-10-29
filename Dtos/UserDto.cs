using System.ComponentModel.DataAnnotations;

namespace miniAPI.Dtos;

/// <summary>
/// Dataoverføringsobjekt for intern bruk.
/// </summary>
public class UserDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Brukernavn må fylles ut")]
    public string Username { get; set; } = string.Empty;
}
