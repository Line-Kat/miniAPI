using Microsoft.AspNetCore.Mvc;
using miniAPI.Dtos;
using miniAPI.Services;

namespace miniAPI.Controllers;

/// <summary>
/// Controller som håndterer registrering og innlogging av brukere.
/// Bruker AuthService til å utføre forretningslogikk.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    /// <summary>
    /// AuthService injiseres via dependency injection og lagres i et readonly felt.
    /// Dette gir controlleren tilgang til forretningslogikken uten å være ansvarlig for instansieringen.
    /// </summary>
    private readonly AuthService _authService;
    private readonly TokenService _tokenService;

    public AuthController(AuthService authService, TokenService tokenService)
    {
        _authService = authService;
        _tokenService = tokenService;
    }

    /// <summary>
    /// Registrerer en ny bruker dersom brukernavnet ikke allerede er i bruk.
    /// Returnerer 409 Conflict ved duplikat, eller 201 Created med brukerdata.
    /// Når brukeren har registrert seg vil hen også logges inn.
    /// </summary>
    [HttpPost("register")]
    public async Task<ActionResult<LoginResponseDto>> Register(RegisterDto dto)
    {
        var success = await _authService.RegisterAsync(dto.Username, dto.Password);
        if (!success) return Conflict("Brukernavnet er allerede i bruk");

        var user = await _authService.LoginAsync(dto.Username, dto.Password);
        var token = _tokenService.CreateToken(user!);

        var response = new LoginResponseDto
        {
            Username = dto.Username,
            Token = token
        };

        return CreatedAtAction(nameof(Register), response);
    }

    /// <summary>
    /// logger inn en bruker ved å verifisere brukernavn og passord.
    /// Returnerer 401 Unauthorized ved feil, ellers 200 Ok med brukerdata.
    /// </summary>
    [HttpPost("login")]
    public async Task<ActionResult<LoginResponseDto>> Login(LoginDto dto)
    {
        var user = await _authService.LoginAsync(dto.Username, dto.Password);
        if (user == null) return Unauthorized("Feil brukernavn eller passord");

        var token = _tokenService.CreateToken(user);
        return Ok(new LoginResponseDto { Username = dto.Username, Token = token });
    }
}
