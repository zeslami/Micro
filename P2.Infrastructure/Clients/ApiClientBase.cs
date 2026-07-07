using System.Net;
using System.Net.Http.Json;
using P2.Domain.Exceptions;

namespace P2.Infrastructure.Clients;

/// <summary>
/// Shared helpers for talking to P1 over HttpClient + JSON, and for translating
/// P1's HTTP status codes into gateway-level exceptions that the ExceptionHandlingMiddleware understands.
/// </summary>
public abstract class ApiClientBase
{
    protected readonly HttpClient HttpClient;

    protected ApiClientBase(HttpClient httpClient)
    {
        HttpClient = httpClient;
    }

    protected async Task<TResponse> GetAsync<TResponse>(string url, CancellationToken ct)
    {
        var response = await HttpClient.GetAsync(url, ct);
        return await ReadOrThrowAsync<TResponse>(response, ct);
    }

    protected async Task<TResponse> PostAsync<TRequest, TResponse>(string url, TRequest body, CancellationToken ct)
    {
        var response = await HttpClient.PostAsJsonAsync(url, body, ct);
        return await ReadOrThrowAsync<TResponse>(response, ct);
    }

    protected async Task PutAsync<TRequest>(string url, TRequest body, CancellationToken ct)
    {
        var response = await HttpClient.PutAsJsonAsync(url, body, ct);
        await EnsureSuccessOrThrowAsync(response, ct);
    }

    protected async Task DeleteAsync(string url, CancellationToken ct)
    {
        var response = await HttpClient.DeleteAsync(url, ct);
        await EnsureSuccessOrThrowAsync(response, ct);
    }

    private static async Task<TResponse> ReadOrThrowAsync<TResponse>(HttpResponseMessage response, CancellationToken ct)
    {
        await EnsureSuccessOrThrowAsync(response, ct);

        var result = await response.Content.ReadFromJsonAsync<TResponse>(cancellationToken: ct);

        return result ?? throw new GatewayException("P1 returned an empty or unparsable response body.");
    }

    private static async Task EnsureSuccessOrThrowAsync(HttpResponseMessage response, CancellationToken ct)
    {
        if (response.IsSuccessStatusCode) return;

        var body = await response.Content.ReadAsStringAsync(ct);

        throw response.StatusCode switch
        {
            HttpStatusCode.NotFound => new RemoteNotFoundException(body),
            HttpStatusCode.Conflict => new RemoteConflictException(body),
            HttpStatusCode.Unauthorized => new RemoteUnauthorizedException(body),
            _ => new GatewayException($"P1 returned {(int)response.StatusCode}: {body}")
        };
    }
}
