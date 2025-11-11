namespace miniAPI.Dtos;

/// <summary>
/// Dataoverføringsobjekt for å vise en oppgave.
/// </summary>
public class ToDoItemDto
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public DateTime Created { get; set; }
}
