using Microsoft.EntityFrameworkCore;
using miniAPI.Models;

namespace miniAPI.Repositories {

    /// <summary>
    /// Klassen definerer hvordan applikasjonen kommuniserer med databasen.
    /// Klassen arver fra DbContext som er EF Core sin baseklasse for databasekommunikasjon.
    /// Klassen representerer prosjektets database som et objekt og gir tilgang til tabeller via  DbSet.
    /// </summary>
    public class OppgaveContext : DbContext {
        ///<summary>
        /// Konstruktøren tar inn konfigurasjon.
        /// base(option) sender konfigurasjonen videre til DbContext, som bruker den til å koble til riktig database.
        /// </summary>
        public OppgaveContext(DbContextOptions<OppgaveContext> options) : base(options) { } // Konstruktør

        /// <summary>
        /// En tabell med navn Oppgaver.
        /// DbSet<Oppgave> betyr at det kan gjøres CRUD-operasjoner på Oppgave-objektet.
        /// EF Core bruker denne linjen til å mappe C#-klassen Oppgave til len database-tabell.
        /// </summary>
        public DbSet<Oppgave> Oppgaver { get; set;}
    }
}
