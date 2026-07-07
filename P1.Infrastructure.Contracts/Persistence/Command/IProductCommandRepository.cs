using P1.Domain.Entities;

namespace P1.Infrastructure.Contracts.Persistence.Command;

public interface IProductCommandRepository
{
    Task AddAsync(Product product, CancellationToken ct = default);
    void Update(Product product);
    void Remove(Product product);
}
