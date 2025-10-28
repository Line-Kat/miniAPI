using Microsoft.AspNetCore.Mvc;
using miniAPI.Models;
using miniAPI.Services;

namespace miniAPI.Controllers {
        /// <summary>
    /// Klasse som håndterer HTTP-forespørsler.
    /// Bruker ToDoItemService til å utføre forretningslogikk.
    /// Attributtene angir at denne klassen er en API-controller, spesifiserer hvilke HTTP-ruter den skal håndtere,
    /// og at metodene returnerer JSON.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ToDoItemController : ControllerBase {
        // ToDoItemService injiseres via dependency injection og lagres i et readonly-felt.
        // Dette gir controlleren tilgang til forretningslogikken uten å være ansvarlig for instansiering.
        private readonly ToDoItemService _service;

        // Kontruktør
        public ToDoItemController(ToDoItemService service) {
            _service = service;
        }

        /// <summary>
        /// Metode som returnerer alle oppgaver.
        /// Attributten angir at dette er en GET-metode.
        /// ActionResult er en returtype som brukes i controller-metoder for å representere et HTTP-svar.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<ToDoItem>>> GetAll() {
            var toDoItems = await _service.GetAll();
            return Ok(toDoItems);
        }

        /// <summary>
        /// Metode som returnerer en bestemt oppgave basert på ID eller NotFound om oppgaven ikke finnes.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoItem>> GetToDoItemById(int id) {
            var toDoItem = await _service.GetToDoItemById(id);
            if(toDoItem == null) return NotFound();
            
            return Ok(toDoItem);
        }

        /// <summary>
        /// Metode som oppretter en ny oppgave.
        /// CreatedAtAction returnerer statuskode 201 Created, en Location-header med URL til den nye ressursen og selve objektet.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ToDoItem>> AddToDoItem(Models.ToDoItem toDoItem) {
            var newToDoItem = await _service.AddToDoItem(toDoItem);
            
            return CreatedAtAction(
                nameof(GetToDoItemById), 
                new {id = newToDoItem.Id},
                newToDoItem
                );
        }

        /// <summary>
        /// Metoden sletter en oppgave basert på ID.
        /// Returnerer 404 dersom oppgaven ikke finnes.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ToDoItem>> DeleteToDoItemById(int id) {
            var deleted = await _service.DeleteToDoItemById(id);
            if(deleted == null) return NotFound();

            return Ok(deleted);
        }
    }
}

