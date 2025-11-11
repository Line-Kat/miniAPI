using System.ComponentModel.DataAnnotations;

namespace miniAPI.Dtos;

/// <summary>
/// Dataoverføringsobjekt for å lage en ny oppgave
/// Trenger kun feltet Title fordi Id og Created settes automatisk.
/// </summary>
public class CreateToDoItemDto
{
    [Required(ErrorMessage = "Tittel på oppgaven må fylles ut")]
    public string Title { get; set; } = string.Empty;
}
