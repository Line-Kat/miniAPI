using Microsoft.EntityFrameworkCore;
using miniAPI.Models;

namespace miniAPI.Repositories;

/// <summary>
/// Implementasjon av IUserRepository som håndterer datatilgang for User-objekter.
/// Bruker Entity Framework til å hente, lagre og validere brukere i databasen.
/// </summary>
public class UserRepository : IUserRepository
{
    private readonly ToDoItemContext _context;

    /// <summary>
    /// Initialiserer UserRepository med en databasekontekst.
    /// Representerer en aktiv forbindelse med databasen, gir tilgang til databasen via Entity Framework..
    /// </summary>
    public UserRepository(ToDoItemContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Henter en bruker basert på brukernavn.
    /// Brukes ved innlogging for å hente passorddate og validere at brukeren finnes.
    /// </summary>
    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
    }

    /// <summary>
    /// Henter bruker basert på Id.
    /// Brukes etter innlogging for intern datatilgang.
    /// </summary>
    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    /// <summary>
    /// Sjekker om et brukernavn allerede finnes i databasen.
    /// Brukes ved registrering for å forhindre duplikater.
    /// </summary>
    public async Task<bool> ExistsAsync(string username)
    {
        return await _context.Users.AnyAsync(u => u.Username == username);
    }

    /// <summary>
    /// Lagrer en ny bruker i databasen.
    /// Brukes ved registrering etter at validering og passord-hashing er utført.
    /// </summary>
    public async Task CreateUserAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }
}