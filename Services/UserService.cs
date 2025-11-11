using miniAPI.Repositories;
using miniAPI.Models;

namespace miniAPI.Services;

/// <summary>
/// Service som håndterer brukerrelaterte operasjoner.
/// </summary>
public class UserService
{
    private readonly IUserRepository _userRepo;

    public UserService(IUserRepository userRepo)
    {
        _userRepo = userRepo;
    }

    /// <summary>
    /// Henter en bruker basert på brukernavn.
    /// </summary>
    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        return await _userRepo.GetByUsernameAsync(username);
    }

    /// <summary>
    /// Sletter en bruker og returnerer det slettede objektet.
    /// </summary>
    public async Task<User?> DeleteUserAsyncByUsername(string username)
    {
        var user = await _userRepo.GetByUsernameAsync(username);
        if (user == null) return null;

        await _userRepo.DeleteUserAsync(user);
        return user;
    }
}
