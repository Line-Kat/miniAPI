using miniAPI.Models;

/// <summary>
/// Interface som definerer hvilke metoder en klasse skal implementere for brukerhåndtering.
/// Skiller hva koden skal gjøre fra hvordan det gjøres.
/// Task indikerer at metodene er asynkrone. 
/// </summary>
public interface IUserRepository {
    /// <summary>
    /// Henter en bruker basert på brukernavn.
    /// </summary>
    Task<User?> GetByUsernameAsync(string username);

    /// <summary>
    /// Brukes etter innlogging for å hente brukerprofil eller relaterte data
    /// </summary>
    Task<User?> GetByIdAsync(int id);

    /// <summary>
    /// Sjekker om et brukernavn allerede finnes i databasen.
    /// </summary>
    Task<bool> ExistsAsync(string username);

    /// <summary>
    /// Lagrer ny bruker i databasen.
    /// Brukes ved registrering.
    /// </summary>
    Task CreateUserAsync(User user);
}
