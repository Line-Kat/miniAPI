using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Klasse som håndterer forettningslogikken.
/// </summary>

public class OppgaveService {
    
    /// <summary>
    /// Deklarerer en liste for oppgaver. Nye oppgaver som opprettes legges til i denne listen.
    /// Listen er readonly som betyr at referansen til listen ikke kan endres, men innholdet i listen kan endres.
    /// </summary>
    private readonly List<Oppgave> _oppgaver = new();

    /// <summary>
    /// En int som brukes når en oppgaves ID skal settes.
    /// </summary>
    private int _nesteId = 1;

    /// <summary>
    /// Metode som returnerer alle oppgavene i listen
    /// </summary>
    public List<Oppgave> HentAlle() {
        return _oppgaver;
    }

    /// <summary>
    /// Metode som returnerer en bestemt oppgave basert på ID.
    /// Spørsmålstegnet etter typen betyr at oppgaven hentes om den finnes ellers returneres null.
    /// </summary>
    public Oppgave? HentOppgaveEtterId(int id) {
        return _oppgaver.FirstOrDefault( o => o.Id == id);
    }

    /// <summary>
    /// Metode som legger oppgave til i listen.
    /// </summary>
    public Oppgave LeggTilOppgave( Oppgave oppgave ) {
        oppgave.Id = _nesteId++; // Uten database, settes Id manuelt i Service-klassen
        _oppgaver.Add( oppgave );

        return oppgave;
    }

    /// <summary>
    /// Metode som sletter en oppgave basert på Id dersom den finnes i listen.
    /// Henter først oppgaven etter Id, dersom det ikke returneres en oppgave, returnerer metoden null.
    /// Dersom oppgaven finnes, fjernes den fra listen.
    /// </summary>
    public Oppgave? SlettOppgaveEtterId(int id) {
        // Lokal variabel for å holde på resultatet som enten er oppgaven eller null.
        var oppgave = _oppgaver.FirstOrDefault( o => o.Id == id );
        if ( oppgave == null ) return null;

        _oppgaver.Remove( oppgave );
        
        return oppgave;
    }
}
