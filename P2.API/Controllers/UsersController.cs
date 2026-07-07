using Microsoft.AspNetCore.Mvc;
using P2.Application.Contracts.Services;
using System.Threading;
using System.Threading.Tasks;

namespace P2.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IGatewayProxyService _proxyService;

    public UsersController(IGatewayProxyService proxyService)
    {
        _proxyService = proxyService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var response = await _proxyService.GetAsync("api/users", ct);
        var content = await response.Content.ReadAsStringAsync(ct);
        return StatusCode((int)response.StatusCode, content);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var response = await _proxyService.GetAsync($"api/users/{id}", ct);
        var content = await response.Content.ReadAsStringAsync(ct);
        return StatusCode((int)response.StatusCode, content);
    }
}
