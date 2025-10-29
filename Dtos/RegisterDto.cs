using System.ComponentModel.DataAnnotations;

namespace miniAPI.Dtos;

/// <summary>
/// Dataoverføringsobjekt for registrering av ny bruker.
/// </summary>
public class RegisterDto
{
    [Required(ErrorMessage = "Brukernavn må fylles ut")]
    [MinLength(3, ErrorMessage = "Brukernavn må være minst tre tegn")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Passord må fylles ut")]
    [MinLength(6, ErrorMessage = "Passordet må inneholde minst seks tegn")]
    public string Password { get; set; } = string.Empty;
}
