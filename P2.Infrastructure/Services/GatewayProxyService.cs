using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using P2.Application.Contracts.Services;

namespace P2.Infrastructure.Services;

public class GatewayProxyService : IGatewayProxyService
{
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GatewayProxyService(
        HttpClient httpClient,
        IHttpContextAccessor httpContextAccessor)
    {
        _httpClient = httpClient;
        _httpContextAccessor = httpContextAccessor;
    }

    private void CopyAuthorizationHeader()
    {
        var httpContext = _httpContextAccessor.HttpContext;

        if (httpContext is null)
            return;

        var authHeader = httpContext.Request.Headers[HeaderNames.Authorization].ToString();

        _httpClient.DefaultRequestHeaders.Authorization = null;

        if (!string.IsNullOrWhiteSpace(authHeader) &&
            AuthenticationHeaderValue.TryParse(authHeader, out var headerValue))
        {
            _httpClient.DefaultRequestHeaders.Authorization = headerValue;
        }
    }

    public Task<HttpResponseMessage> GetAsync(
        string path,
        CancellationToken cancellationToken = default)
    {
        CopyAuthorizationHeader();
        return _httpClient.GetAsync(path, cancellationToken);
    }

    public Task<HttpResponseMessage> DeleteAsync(
        string path,
        CancellationToken cancellationToken = default)
    {
        CopyAuthorizationHeader();
        return _httpClient.DeleteAsync(path, cancellationToken);
    }

    public Task<HttpResponseMessage> PostAsync<T>(
        string path,
        T body,
        CancellationToken cancellationToken = default)
    {
        CopyAuthorizationHeader();
        return _httpClient.PostAsJsonAsync(path, body, cancellationToken);
    }

    public Task<HttpResponseMessage> PutAsync<T>(
        string path,
        T body,
        CancellationToken cancellationToken = default)
    {
        CopyAuthorizationHeader();
        return _httpClient.PutAsJsonAsync(path, body, cancellationToken);
    }
}
