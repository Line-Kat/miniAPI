using System.ComponentModel.DataAnnotations;

namespace miniAPI.Models;

/// <summary>
/// Modell for innlogging. Validerer brukerens input før forespørselen behandles.
/// Sikrer at nødvendige felt er utfylt og beskytter systemet mot ugyldige innloggingsforsøk.
/// </summary>
public class LoginRequest
{
    /// <summary>
    /// Bruker [Required] for å sikre at nødvendige felt er utfylt før forespørselen behandles.
    /// Valideringen beskytter systemet mot ugyldige innloggingsforsøk og bidrar til dataintegritet.
    /// </summary>
    [Required(ErrorMessage = "Brukernavn må fylles ut!")]
    public string Username { get; set; } = string.Empty;
    [Required(ErrorMessage = "Passord må fylles ut!")]
    public string Password { get; set; } = string.Empty;
}