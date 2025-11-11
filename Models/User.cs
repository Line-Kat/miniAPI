using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema;

namespace miniAPI.Models;

/// <summary>
/// Representerer en bruker i databasen med lagret passord som hash og salt.
/// </summary>
public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Hash av brukerens passord, generert med HMACHASH512.
    /// </summary>
    public byte[] PasswordHash { get; set; } = Array.Empty<byte>();

    /// <summary>
    /// Salt brukt ved hashinh av passordet.
    /// </summary>
    public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();
}