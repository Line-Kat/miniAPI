using Microsoft.AspNetCore.Mvc;
using miniAPI.Dtos;
using miniAPI.Services;

namespace miniAPI.Controllers;

/// <summary>
/// Håndterere brukerdata.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Returnerer brukerdata basert på brujernavn.
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpGet("{username}")]
    public async Task<ActionResult<PublicUserDto>> GetUserByUsernameAsync(string username)
    {
        var user = await _userService.GetUserByUsernameAsync(username);
        if (user == null) return NotFound("Bruker ikke funnet");

        var response = new PublicUserDto { Username = user.Username };
        return Ok(response);

    }

    /// <summary>
    /// Sletter en bruker basert på brukernavn.
    /// Returnerer 404 hvis brukeren ikke finnes, ellers 200 Ok med slette brukerdata.
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    [HttpDelete("{username}")]
    public async Task<ActionResult<PublicUserDto>> DeleteUserByUsername(string username)
    {
        var deletedUser = await _userService.DeleteUserAsyncByUsername(username);
        if (deletedUser == null) return NotFound("Brukeren ikke funnet");

        var response = new PublicUserDto { Username = deletedUser.Username };
        return Ok(response);
    }
}
