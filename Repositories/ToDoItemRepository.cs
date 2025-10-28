using Microsoft.EntityFrameworkCore;
using miniAPI.Models;

namespace miniAPI.Repositories {

    /// <summary>
    /// Klasse for CRUD-operasjoner på Oppgave-tabellen.
    /// Bruker OppgaveContext for å kommunisere med databasen.
    /// </summary>
    public class ToDoItemRepository {
        
        /// <summary>
        /// OppgaveContext injiseres via dependency injection og lagres i et readonly-felt (_context).
        /// Dette gir repository tilgang til databasetilkobling uten å være ansvarlig for instansiering.
        /// </summary>
        private readonly ToDoItemContext _context;

        /// <summary>
        /// Konstruktør som mottar OppgaveContext via Dependency Injection.
        /// </summary>
        public ToDoItemRepository(ToDoItemContext context) {
            _context = context;
        }

        /// <summary>
        /// Asynkron metode som henter alle oppgaver fra databasen.
        /// Bruker EF Core og ToListAsync for å hente data uten å blokkere hovedtråden.
        /// EF Core sin ToListAsync()-metode returnerer aldri null.
        /// Den returnerer alltid en gyldig liste, enten med elementer, eller som en tom liste hvis tabellen er tom.
        /// Task&lt;T&gt; representerer en operasjon som kjører i bakgrunnen og returnerer et resultat av typen T.
        /// </summary>
        public async Task<List<ToDoItem>> GetAllAsync() {
            return await _context.ToDoItems.ToListAsync();
        }

        /// <summary>
        /// Asynkron metode som returnerer en bestemt oppgave basert på ID.
        /// Returnerer Oppgave-objektet hvis det finnes, ellers null.
        /// Bruker EF Core sin FindAsync()-metode, som returnerer null hvis ID ikke finnes.
        /// </summary>
        public async Task<ToDoItem?> GetToDoItemByIdAsync(int id) {
            return await _context.ToDoItems.FindAsync(id);
        }

        /// <summary>
        /// Asynkron metode som legger til oppgave i databasen.
        /// </summary>
        public async Task<ToDoItem> AddToDoItemAsync(Models.ToDoItem toDoItem) {
            await _context.ToDoItems.AddAsync(toDoItem);
            await _context.SaveChangesAsync();

            return toDoItem;
        }

        /// <summary>
        /// Asynkron metode som sletter en oppgave fra databasen.
        /// Først hentes oppgaven basert på ID. Dersom den ikke finnes, returneres null.
        /// Hvis oppgaven finnes, slettes den og endringen lagres.
        /// </summary>
        public async Task<ToDoItem?> DeleteToDoItemByAsync(int id) {
            var toDoItem = await _context.ToDoItems.FindAsync(id);

            if(toDoItem == null) return null;

            _context.ToDoItems.Remove(toDoItem);
            await _context.SaveChangesAsync();

            return toDoItem;
        }
    }
}
