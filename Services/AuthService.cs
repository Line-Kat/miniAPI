using System.Security.Cryptography;
using System.Text;
using miniAPI.Models;

namespace miniAPI.Services;

/// <summary>
/// AuthService håndterer registrering og innlogging av brukere.
/// Bruker IUserRepository for datatilgang og HMACSHA512 for sikker passordshåndtering.
/// HMACSHA512 er en algoritme som genererer en unik, kryptografisk kode av et passord, basert på en hemmelig nøkkel (salt).
/// </summary>
public class AuthService
{
    private readonly IUserRepository _userRepo;

    /// <summary>
    /// Initialiserer AuthService med tilgang til brukerdata via IUserRepository.
    /// Bruken av interface gir løs kobling mellom AuthService og datatilgang. 
    /// I Program.cs er det konfigurert at IUserRepository skal injiseres som UserRepository.
    /// </summary>
    public AuthService(IUserRepository userRepo)
    {
        _userRepo = userRepo;
    }

    /// <summary>
    /// Registrerer en ny bruker hvis brukernavnet ikke finnes fra før.
    /// Passordet hashes med en unik salt før det lagres i databasen.
    /// </summary>
    public async Task<bool> RegisterAsync(string username, string password)
    {
        if (await _userRepo.ExistsAsync(username)) return false;

        CreatePasswordHash(password, out byte[] hash, out byte[] salt);
        var user = new User { Username = username, PasswordHash = hash, PasswordSalt = salt };
        await _userRepo.CreateUserAsync(user);
        return true;
    }

    /// <summary>
    /// Logger inn en bruker ved å verifisere passordet mot lagret hash og salt.
    /// </summary>
    public async Task<User?> LoginAsync(string username, string password)
    {
        var user = await _userRepo.GetByUsernameAsync(username);
        if (user == null || !VerifyPassword(password, user.PasswordHash, user.PasswordSalt)) return null;
        return user;
    }

    /// <summary>
    /// Oppretter en passord-hash og salt ved hjelp av HMACSHA512.
    /// Salt lagres separat for å sikre unik hashing per bruker.
    /// Funksjonen tar inn ett passord og returnerer hash og salt via out-parametre.
    /// </summary>
    private void CreatePasswordHash(string password, out byte[] hash, out byte[] salt)
    {
        using var hmac = new HMACSHA512();
        salt = hmac.Key;
        hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }

    /// <summary>
    /// Verifiserer om et gitt passord matcher lagret hash og salt.
    /// Returnerer true dersom hashen som genereres fra det innskrevne passordet er identisk med den lagrede hashen. 
    /// </summary>
    private bool VerifyPassword(string password, byte[] hash, byte[] salt)
    {
        // Lager HMACSHA512-verktøy med brukerens lagrede salt som nøkkel.
        // Bruker samme salt som ved registrering for å få identisk hash.
        // Using sørger for at objektet (hmac) automatisk ryddes opp. 
        using var hmac = new HMACSHA512(salt);

        // Genererer hash av det innskrevne passordet.
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

        // Sammenligner generert hash med lagret hash for å verifisere passordet.
        return computedHash.SequenceEqual(hash);
    }
}
