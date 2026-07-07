using P1.Domain.Entities;

namespace P1.Infrastructure.Contracts.Persistence.Command;

public interface IProductGroupCommandRepository
{
    Task AddAsync(ProductGroup productGroup, CancellationToken ct = default);
    void Update(ProductGroup productGroup);
    void Remove(ProductGroup productGroup); // از Remove به جای Delete استفاده می‌کنیم تا با ساختار کل پروژه هماهنگ باشد
}
