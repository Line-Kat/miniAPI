using System.ComponentModel.DataAnnotations;

namespace miniAPI.Models;

/// <summary>
/// Modell for registrering av ny bruker.
/// Bruker [Required] for å sikre at nødvendige felt er utfylt før forespørselen behandles.
/// Dette beskytter systemet mot ugyldige registreringsforsøk og bidrar til dataintegritet.
/// </summary>
public class RegisterRequest
{
    [Required(ErrorMessage = "Brukernavn må fylles ut")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Passord må fylles ut")]
    public string Password { get; set; } = string.Empty;
}