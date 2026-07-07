using Microsoft.AspNetCore.Mvc;
using P1.Application.Contracts.Common;
using Microsoft.Extensions.Localization;
using P1.API.Resources;

namespace P1.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    protected readonly IStringLocalizer<SharedResource> Localizer;

    protected ApiControllerBase(IStringLocalizer<SharedResource> localizer)
    {
        Localizer = localizer;
    }
    protected IActionResult HandleResult(Result result)
    {
        if (result.IsSuccess)
            return Ok();

        return BadRequest(new { Error = result.Error });
    }

    protected IActionResult HandleResult<T>(Result<T> result)
    {
        if (result.IsSuccess)
            return Ok(result.Value);

        return BadRequest(new { Error = result.Error });
    }
}
