using System.Collections.Generic;
using System.Linq;
using miniAPI.Models;
using miniAPI.Repositories;

namespace miniAPI.Services {
        /// <summary>
    /// Klasse som håndterer forretningslogikken.
    /// </summary>
    public class ToDoItemService {

		/// <summary>
        /// OppgaveRepository injiseres via dependency injection og lagres i et readonly-felt.
		/// Dette gir service tilgang til repository uten å være ansvarlig for instansiering.
        /// </summary>
		private readonly ToDoItemRepository _repository;

        // Konstruktør
        public ToDoItemService(ToDoItemRepository repository) {
            _repository = repository;
        }

        
        /// <summary>
        /// Deklarerer en liste for oppgaver. Nye oppgaver som opprettes legges til i denne listen.
        /// Listen er readonly som betyr at referansen til listen ikke kan endres, men innholdet i listen kan endres.
        /// </summary>
        //private readonly List<Oppgave> _oppgaver = new(); // Midlertidig lagring, før implementering av database.

        /// <summary>
        /// En int som brukes når en oppgaves ID skal settes.
        /// </summary>
        //private int _nesteId = 1; Fjerner denne kodelinjen fordi jeg ønsker at Id skal settes automatisk.

        /// <summary>
        /// Metode som returnerer alle oppgavene i listen
        /// </summary>
        public async Task<List<ToDoItem>> GetAll() {
            return await _repository.GetAllAsync();
        }

        /// <summary>
        /// Metode som returnerer en bestemt oppgave basert på ID.
        /// Spørsmålstegnet etter typen betyr at oppgaven hentes om den finnes ellers returneres null.
        /// </summary>
        public async Task<ToDoItem?> GetToDoItemById(int id) {
            return await _repository.GetToDoItemByIdAsync(id);
        }

        /// <summary>
        /// Metode som legger oppgave til i listen.
        /// </summary>
        public async Task<ToDoItem> AddToDoItem(Models.ToDoItem oppgave ) {
            // oppgave.Id = _nesteId++; // Uten database, settes Id manuelt i Service-klassen
            //_oppgaver.Add(oppgave);

            return await _repository.AddToDoItemAsync(oppgave);
        }

        /// <summary>
        /// Metode som sletter en oppgave basert på Id dersom den finnes i listen.
        /// Henter først oppgaven etter Id, dersom det ikke returneres en oppgave, returnerer metoden null.
        /// Dersom oppgaven finnes, fjernes den fra listen.
        /// </summary>
        
        public async Task<ToDoItem?> DeleteToDoItemById(int id) {
            // Lokal variabel for å holde på resultatet som enten er oppgaven eller null.
            
            /// <summary>
            /// Under er kode før jeg implementerte database
            /// var oppgave = _oppgaver.FirstOrDefault( o => o.Id == id );
            ///if ( oppgave == null ) return null;

            /// _oppgaver.Remove(oppgave);
            /// </summary>
            
            return await _repository.DeleteToDoItemByAsync(id);
        }
    }
}
