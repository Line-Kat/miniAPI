using Microsoft.EntityFrameworkCore;
using miniAPI.Models;

namespace miniAPI.Repositories {

    /// <summary>
    /// Klasse for CRUD-operasjoner på Oppgave-tabellen.
    /// Bruker OppgaveContext for å kommunisere med databasen.
    /// </summary>
    public class OppgaveRepository {
        
        /// <summary>
        /// OppgaveContext injiseres via dependency injection og lagres i et readonly-felt (_context).
        /// Dette gir repository tilgang til databasetilkobling uten å være ansvarlig for instansiering.
        /// </summary>
        private readonly OppgaveContext _context;

        /// <summary>
        /// Konstruktør som mottar OppgaveContext via Dependency Injection.
        /// </summary>
        public OppgaveRepository(OppgaveContext context) {
            _context = context;
        }

        /// <summary>
        /// Asynkron metode som henter alle oppgaver fra databasen.
        /// Bruker EF Core og ToListAsync for å hente data uten å blokkere hovedtråden.
        /// EF Core sin ToListAsync()-metode returnerer aldri null.
        /// Den returnerer alltid en gyldig liste, enten med elementer, eller som en tom liste hvis tabellen er tom.
        /// Task&lt;T&gt; representerer en operasjon som kjører i bakgrunnen og returnerer et resultat av typen T.
        /// </summary>
        public async Task<List<Oppgave>> HentAlleAsync() {
            return await _context.Oppgaver.ToListAsync();
        }

        /// <summary>
        /// Asynkron metode som returnerer en bestemt oppgave basert på ID.
        /// Returnerer Oppgave-objektet hvis det finnes, ellers null.
        /// Bruker EF Core sin FindAsync()-metode, som returnerer null hvis ID ikke finnes.
        /// </summary>
        public async Task<Oppgave?> HentOppgaveEtterIdAsync(int id) {
            return await _context.Oppgaver.FindAsync(id);
        }

        /// <summary>
        /// Asynkron metode som legger til oppgave i databasen.
        /// </summary>
        public async Task<Oppgave> LeggTilOppgaveAsync(Oppgave oppgave) {
            _context.Oppgaver.Add(oppgave);
            await _context.SaveChangesAsync();

            return oppgave;
        }

        /// <summary>
        /// Asynkron metode som sletter en oppgave fra databasen.
        /// Først hentes oppgaven basert på ID. Dersom den ikke finnes, returneres null.
        /// Hvis oppgaven finnes, slettes den og endringen lagres.
        /// </summary>
        public async Task<Oppgave?> SlettOppgaveEtterIdAsync(int id) {
            var oppgave = await _context.Oppgaver.FindAsync(id);

            if(oppgave == null) return null;

            _context.Oppgaver.Remove(oppgave);
            await _context.SaveChangesAsync();

            return oppgave;
        }
    }
}
