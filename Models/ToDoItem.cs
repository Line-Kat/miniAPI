using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema;

namespace miniAPI.Models {
    ///<summary>
    /// Representerer en oppgave i systemet.
    /// Inneholder ID, tittel og tidspunkt for opprettelse.
    /// </summary>
    public class ToDoItem {

        /// <summary>
        /// Unik identifikator for oppgaven (primærnøkkel).
        /// Genereres automatisk av databasen via Entity Framework Core.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Tittel for oppgaven.
        /// Validerer at feltet er fylt ut og at input-teksten ikke overskrider satt verdi.
        /// </summary>
        [Required(ErrorMessage = "Oppgaven må ha en tittel.")]
        [StringLength(100, ErrorMessage = "Tittelen kan ikke være lengre enn 100 tegn.")]
        public string Title { get; set; } = string.Empty; // Ved å sette string.Empty får Tittel en standardverdi ("") når et nytt Oppgave-objekt opprettes

        /// <summary>
        /// Dato og klokkeslett for opprettelse av oppgaven settes automatisk.
        /// </summary>
        [Required]
        public DateTime Created { get; set; } = DateTime.UtcNow;
    }
}
