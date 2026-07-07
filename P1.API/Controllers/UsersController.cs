using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using P1.API.Resources;
using P1.Application.Contracts.DTOs;
using P1.Application.Contracts.Services;

namespace P1.API.Controllers;

[Authorize(Roles = "Admin")] // محدودسازی دسترسی مدیریت کاربران به نقش Admin
public class UsersController : ApiControllerBase
{
    private readonly IUserService _userService;

    public UsersController(
       IUserService userService,
       IStringLocalizer<SharedResource> localizer)
       : base(localizer)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _userService.GetAllAsync(cancellationToken);
        return HandleResult(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var result = await _userService.GetByIdAsync(id, cancellationToken);
        return HandleResult(result);
    }

    [HttpPost]
    [AllowAnonymous] // امکان ثبت‌نام اولیه بدون نیاز به توکن
    public async Task<IActionResult> Create([FromBody] CreateUserDto createDto, CancellationToken cancellationToken)
    {
        var result = await _userService.CreateAsync(createDto, cancellationToken);
        return HandleResult(result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateUserDto updateDto, CancellationToken cancellationToken)
    {
        var result = await _userService.UpdateAsync(id, updateDto, cancellationToken);
        return HandleResult(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await _userService.DeleteAsync(id, cancellationToken);
        return HandleResult(result);
    }
}
