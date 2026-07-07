using Microsoft.AspNetCore.Mvc;
using P2.Application.Contracts.DTOs;
using P2.Application.Contracts.DTOs.Auth;
using P2.Application.Contracts.Services;
using System.Threading;
using System.Threading.Tasks;

namespace P2.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IGatewayProxyService _proxyService;

    public AuthController(IGatewayProxyService proxyService)
    {
        _proxyService = proxyService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto dto, CancellationToken ct)
    {
        var response = await _proxyService.PostAsync("api/auth/register", dto, ct);
        var content = await response.Content.ReadAsStringAsync(ct);
        return StatusCode((int)response.StatusCode, content);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto dto, CancellationToken ct)
    {
        var response = await _proxyService.PostAsync("api/auth/login", dto, ct);
        var content = await response.Content.ReadAsStringAsync(ct);
        return StatusCode((int)response.StatusCode, content);
    }
}
