using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using P1.API.Controllers;
using P1.API.Resources;
using P1.Application.Contracts.DTOs;
using P1.Application.Contracts.Services;

namespace P1.API.Controllers;

[Authorize]
public class ProductsController : ApiControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(
       IProductService productService,
       IStringLocalizer<SharedResource> localizer)
       : base(localizer)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _productService.GetAllAsync(cancellationToken);
        return HandleResult(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var result = await _productService.GetByIdAsync(id, cancellationToken);
        return HandleResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductDto createDto, CancellationToken cancellationToken)
    {
        var result = await _productService.CreateAsync(createDto, cancellationToken);
        return HandleResult(result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateProductDto updateDto, CancellationToken cancellationToken)
    {
        var result = await _productService.UpdateAsync(id, updateDto, cancellationToken);
        return HandleResult(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await _productService.DeleteAsync(id, cancellationToken);
        return HandleResult(result);
    }

    [HttpGet("group/{groupId:int}")]
    public async Task<IActionResult> GetByGroupId(int groupId, CancellationToken cancellationToken)
    {
        var result = await _productService.GetProductsByGroupIdAsync(groupId, cancellationToken);
        return HandleResult(result);
    }
}
