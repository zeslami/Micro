using Microsoft.AspNetCore.Mvc;
using P2.Application.Contracts.DTOs.ProductGroups;
using P2.Application.Contracts.Services;
using System.Threading;
using System.Threading.Tasks;

namespace P2.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductGroupsController : ControllerBase
{
    private readonly IGatewayProxyService _proxyService;

    public ProductGroupsController(IGatewayProxyService proxyService)
    {
        _proxyService = proxyService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var response = await _proxyService.GetAsync("api/productgroups", ct);
        var content = await response.Content.ReadAsStringAsync(ct);
        return StatusCode((int)response.StatusCode, content);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var response = await _proxyService.GetAsync($"api/productgroups/{id}", ct);
        var content = await response.Content.ReadAsStringAsync(ct);
        return StatusCode((int)response.StatusCode, content);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductGroupDto dto, CancellationToken ct)
    {
        var response = await _proxyService.PostAsync("api/productgroups", dto, ct);
        var content = await response.Content.ReadAsStringAsync(ct);
        return StatusCode((int)response.StatusCode, content);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateProductGroupDto dto, CancellationToken ct)
    {
        var response = await _proxyService.PutAsync($"api/productgroups/{id}", dto, ct);
        var content = await response.Content.ReadAsStringAsync(ct);
        return StatusCode((int)response.StatusCode, content);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var response = await _proxyService.DeleteAsync($"api/productgroups/{id}", ct);
        var content = await response.Content.ReadAsStringAsync(ct);
        return StatusCode((int)response.StatusCode, content);
    }
}
