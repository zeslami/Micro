using P2.Application.Contracts.DTOs.Users;

namespace P2.Infrastructure.Contracts.Clients;

/// <summary>Forwards requests to P1's UsersController.</summary>
public interface IUserApiClient
{
    Task<List<UserDto>> GetAllAsync(CancellationToken ct = default);
    Task<UserDto> GetByIdAsync(int id, CancellationToken ct = default);
}
