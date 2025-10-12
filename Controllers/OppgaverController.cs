using Microsoft.AspNetCore.Mvc;
using miniAPI.Models;
using miniAPI.Services;

namespace miniAPI.Controllers {
        /// <summary>
    /// Klasse som håndterer HTTP-forespørsler.
    /// Bruker OppgaveService til å utføre forretningslogikk.
    /// Attributtene angir at denne klassen er en API-controller, spesifiserer hvilke HTTP-ruter den skal håndtere,
    /// og at metodene returnerer JSON.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class OppgaverController : ControllerBase {
        // OppgaveService injiseres via dependency injection og lagres i et readonly-felt.
        // Dette gir controlleren tilgang til forretningslogikken uten å være ansvarlig for instansiering.
        private readonly OppgaveService _service;

        // Kontruktør
        public OppgaverController( OppgaveService service) {
            _service = service;
        }

        /// <summary>
        /// Metode som returnerer alle oppgaver.
        /// Attributten angir at dette er en GET-metode.
        /// ActionResult er en returtype som brukes i controller-metoder for å representere et HTTP-svar.
        /// </summary>
        [HttpGet]
        public ActionResult<List<Oppgave>> HentAlle() {
            var oppgaver = _service.HentAlle();
            return Ok(oppgaver);
        }

        /// <summary>
        /// Metode som returnerer en bestemt oppgave basert på ID eller NotFound om oppgaven ikke finnes.
        /// </summary>
        [HttpGet("{id}")]
        public ActionResult<Oppgave> HentOppgaveEtterId(int id) {
            var oppgave = _service.HentOppgaveEtterId(id);
            if(oppgave == null) return NotFound();
            
            return Ok(oppgave);
        }

        /// <summary>
        /// Metode som oppretter en ny oppgave.
        /// CreatedAtAction returnerer statuskode 201 Created, en Location-header med URL til den nye ressursen og selve objektet.
        /// </summary>
        [HttpPost]
        public ActionResult<Oppgave> LeggTilOppgave(Oppgave oppgave) {
            var nyOppgave = _service.LeggTilOppgave(oppgave);
            
            return CreatedAtAction(
                nameof(HentOppgaveEtterId), 
                new {id = nyOppgave.Id},
                nyOppgave
                );
        }

        /// <summary>
        /// Metoden sletter en oppgave basert på ID.
        /// Returnerer 404 dersom oppgaven ikke finnes.
        /// </summary>
        [HttpDelete("{id}")]
        public ActionResult<Oppgave> SlettOppgaveEtterId(int id) {
            var slettet = _service.SlettOppgaveEtterId(id);
            if(slettet == null) return NotFound();

            return Ok(slettet);
        }
    }
}

