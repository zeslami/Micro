using Microsoft.AspNetCore.Http;

namespace P2.Infrastructure.Clients;

/// <summary>
/// Copies the inbound "Authorization" header (the JWT the caller sent to P2) onto every
/// outgoing HttpClient request to P1, so P1 sees the same Bearer token and can authorize it itself.
/// Registered on every typed HttpClient via .AddHttpMessageHandler&lt;AuthForwardingHandler&gt;().
/// </summary>
public class AuthForwardingHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthForwardingHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var incomingAuthHeader = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString();

        if (!string.IsNullOrWhiteSpace(incomingAuthHeader))
        {
            request.Headers.TryAddWithoutValidation("Authorization", incomingAuthHeader);
        }

        return base.SendAsync(request, cancellationToken);
    }
}
