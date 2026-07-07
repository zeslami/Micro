using System.Net;
using System.Text.Json;
using FluentValidation;
using P2.Domain.Exceptions;
using Serilog;

namespace P2.API.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        Log.Error(exception, "Unhandled exception in P2 gateway while processing {Path}", context.Request.Path);

        var (statusCode, message, errors) = exception switch
        {
            ValidationException validationEx => (
                HttpStatusCode.BadRequest,
                "One or more validation errors occurred.",
                validationEx.Errors.Select(e => e.ErrorMessage).ToList()),

            RemoteNotFoundException notFoundEx => (
                HttpStatusCode.NotFound,
                notFoundEx.Message,
                new List<string>()),

            RemoteConflictException conflictEx => (
                HttpStatusCode.Conflict,
                conflictEx.Message,
                new List<string>()),

            RemoteUnauthorizedException unauthorizedEx => (
                HttpStatusCode.Unauthorized,
                unauthorizedEx.Message,
                new List<string>()),

            GatewayException gatewayEx => (
                HttpStatusCode.BadGateway,
                gatewayEx.Message,
                new List<string>()),

            _ => (
                HttpStatusCode.InternalServerError,
                "An unexpected error occurred in the gateway. Please try again later.",
                new List<string>())
        };

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
