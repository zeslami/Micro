using System.Net;
using System.Text.Json;
using FluentValidation;
using Microsoft.Extensions.Localization;
using P1.API.Resources;
using P1.Application.Common.Exceptions;
using Serilog;

namespace P1.API.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IStringLocalizer<SharedResource> localizer)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex, localizer);
        }
    }

    private static async Task HandleExceptionAsync(
        HttpContext context,
        Exception exception,
        IStringLocalizer<SharedResource> localizer)
    {
        Log.Error(exception, "Unhandled exception while processing {Path}", context.Request.Path);

        HttpStatusCode statusCode;
        string message;
        List<string> errors;

        switch (exception)
        {
            case ValidationException validationEx:
                statusCode = HttpStatusCode.BadRequest;
                message = localizer["ValidationFailed"].Value;
                errors = validationEx.Errors.Select(e => e.ErrorMessage).ToList();
                break;

            case ConflictException conflictEx:
                statusCode = HttpStatusCode.Conflict;
                message = conflictEx.Message;
                errors = new List<string>();
                break;

            case UnauthorizedAppException unauthorizedEx:
                statusCode = HttpStatusCode.Unauthorized;
                message = unauthorizedEx.Message;
                errors = new List<string>();
                break;

            default:
                statusCode = HttpStatusCode.InternalServerError;
                message = localizer["UnexpectedError"].Value;
                errors = new List<string>();
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var payload = JsonSerializer.Serialize(new
        {
            statusCode = (int)statusCode,
            message,
            errors
        });

        await context.Response.WriteAsync(payload);
    }
}
