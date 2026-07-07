using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using P1.API.Resources;
using P1.Application.Contracts.DTOs;
using P1.Application.Contracts.Services;

namespace P1.API.Controllers;

[Authorize]
public class ProductGroupsController : ApiControllerBase
{
    private readonly IProductGroupService _productGroupService;
   

    public ProductGroupsController(
        IProductGroupService productGroupService,
        IStringLocalizer<SharedResource> localizer)
        : base(localizer)
    {
        _productGroupService = productGroupService;
    }
    

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _productGroupService.GetAllAsync(cancellationToken);
        return HandleResult(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var result = await _productGroupService.GetByIdAsync(id, cancellationToken);
        return HandleResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductGroupDto createDto, CancellationToken cancellationToken)
    {
        var result = await _productGroupService.CreateAsync(createDto, cancellationToken);
        return HandleResult(result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateProductGroupDto updateDto, CancellationToken cancellationToken)
    {
        var result = await _productGroupService.UpdateAsync(id, updateDto, cancellationToken);
        return HandleResult(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await _productGroupService.DeleteAsync(id, cancellationToken);
        return HandleResult(result);
    }
}
