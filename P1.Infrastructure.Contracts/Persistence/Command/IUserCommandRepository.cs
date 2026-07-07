using P1.Domain.Entities;

namespace P1.Infrastructure.Contracts.Persistence.Command;

public interface IUserCommandRepository
{
    Task AddAsync(User user, CancellationToken ct = default);
    void Update(User user);
    void Remove(User user);
}
