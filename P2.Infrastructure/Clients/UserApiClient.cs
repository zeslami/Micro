using P2.Application.Contracts.DTOs.Users;
using P2.Infrastructure.Contracts.Clients;

namespace P2.Infrastructure.Clients;

public class UserApiClient : ApiClientBase, IUserApiClient
{
    public UserApiClient(HttpClient httpClient) : base(httpClient)
    {
    }

    public Task<List<UserDto>> GetAllAsync(CancellationToken ct = default) =>
        GetAsync<List<UserDto>>("api/users", ct);

    public Task<UserDto> GetByIdAsync(int id, CancellationToken ct = default) =>
        GetAsync<UserDto>($"api/users/{id}", ct);
}
