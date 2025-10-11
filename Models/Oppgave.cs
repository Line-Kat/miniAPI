using System;
/// Gir tilgang til valideringsattributter, som [Required] og [StringLength]. 
/// Brukes for å sikre datakvalitet og automatisk input-validering i API-et.
using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema;

///<summary>
/// Representerer en oppgave i systemet.
/// Inneholder ID, tittel og tidspunkt for opprettelse
/// </summary>
public class Oppgave {

    /// <summary>
    /// Unik identifikator for oppgaven (primærnøkkel) som genereres automatisk ved opprettelse når databasen er koblet til via Entity Framework Core.
    /// I nåværende versjon uten database, settes ID manuelt i service-klassen.
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
    public string Tittel { get; set; }

    /// <summary>
    /// Dato og klokkeslett for opprettelse av oppgaven settes automatisk.
    /// </summary>    
    public DateTime Opprettet { get; set; } = DateTime.UtcNow;
    } 
}
