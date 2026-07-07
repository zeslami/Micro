using P1.Domain.Entities;

namespace P1.Infrastructure.Contracts.Persistence.Query;

public interface IProductQueryRepository
{
    Task<Product?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<List<Product>> GetAllAsync(CancellationToken ct = default);
    Task<List<ProductWithGroupView>> GetProductsWithGroupAsync(CancellationToken ct = default);
    Task<List<ProductByGroupResult>> GetProductsByGroupAsync(int productGroupId, CancellationToken ct = default);
    Task<List<Product>> GetByGroupIdAsync(int groupId, CancellationToken ct = default); // اضافه کردن متد گم‌شده
}
