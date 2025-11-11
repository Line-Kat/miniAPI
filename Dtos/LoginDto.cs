using System.ComponentModel.DataAnnotations;

namespace miniAPI.Dtos;

/// <summary>
/// Dataoverføringsobjekt for innlogging
/// </summary>
public class LoginDto
{
    [Required(ErrorMessage = "Brukernavn må fylles ut")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Passord må fylles ut")]
    public string Password { get; set; } = string.Empty;
}
