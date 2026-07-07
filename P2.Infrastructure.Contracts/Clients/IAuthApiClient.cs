using P2.Application.Contracts.DTOs.Auth;

namespace P2.Infrastructure.Contracts.Clients;

/// <summary>Forwards auth requests to P1's AuthController.</summary>
public interface IAuthApiClient
{
    Task<AuthResponseDto> RegisterAsync(RegisterRequestDto dto, CancellationToken ct = default);
    Task<AuthResponseDto> LoginAsync(LoginRequestDto dto, CancellationToken ct = default);
}
