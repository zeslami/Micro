using P2.Application.Contracts.DTOs.Auth;
using P2.Infrastructure.Contracts.Clients;

namespace P2.Infrastructure.Clients;

public class AuthApiClient : ApiClientBase, IAuthApiClient
{
    public AuthApiClient(HttpClient httpClient) : base(httpClient)
    {
    }

    public Task<AuthResponseDto> RegisterAsync(RegisterRequestDto dto, CancellationToken ct = default) =>
        PostAsync<RegisterRequestDto, AuthResponseDto>("api/auth/register", dto, ct);

    public Task<AuthResponseDto> LoginAsync(LoginRequestDto dto, CancellationToken ct = default) =>
        PostAsync<LoginRequestDto, AuthResponseDto>("api/auth/login", dto, ct);

    Task<AuthResponseDto> IAuthApiClient.RegisterAsync(RegisterRequestDto dto, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    Task<AuthResponseDto> IAuthApiClient.LoginAsync(LoginRequestDto dto, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
