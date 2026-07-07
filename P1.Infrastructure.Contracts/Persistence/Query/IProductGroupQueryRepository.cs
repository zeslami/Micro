using P1.Domain.Entities;

namespace P1.Infrastructure.Contracts.Persistence.Query;

public interface IProductGroupQueryRepository
{
    Task<ProductGroup?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<List<ProductGroup>> GetAllAsync(CancellationToken ct = default);
}