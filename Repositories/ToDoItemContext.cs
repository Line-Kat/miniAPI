using Microsoft.EntityFrameworkCore;
using miniAPI.Models;

namespace miniAPI.Repositories {

    /// <summary>
    /// Klassen definerer hvordan applikasjonen kommuniserer med databasen.
    /// Klassen arver fra DbContext som er EF Core sin baseklasse for databasekommunikasjon.
    /// Klassen representerer prosjektets database som et objekt og gir tilgang til tabeller via  DbSet.
    /// </summary>
    public class ToDoItemContext : DbContext {
        ///<summary>
        /// Konstruktøren tar inn konfigurasjon.
        /// base(option) sender konfigurasjonen videre til DbContext, som bruker den til å koble til riktig database.
        /// </summary>
        public ToDoItemContext(DbContextOptions<ToDoItemContext> options) : base(options) { } // Konstruktør

        /// <summary>
        /// En tabell med navn ToDoItems.
        /// DbSet<ToDoItem> betyr at det kan gjøres CRUD-operasjoner på Oppgave-objektet.
        /// EF Core bruker denne linjen til å mappe klassen ToDoItem til en database-tabell.
        /// </summary>
        public DbSet<ToDoItem> ToDoItems { get; set;}
    }
}
