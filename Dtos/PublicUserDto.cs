using System.ComponentModel.DataAnnotations;

namespace miniAPI.Dtos;

/// <summary>
/// Dataoverføringsobjekt for å returnere brukerinformasjon uten sensitive data.
/// </summary>
public class PublicUserDto
{
    [Required(ErrorMessage = "Brukernavn må fylles ut")]
    public string Username { get; set; } = string.Empty;    
}
