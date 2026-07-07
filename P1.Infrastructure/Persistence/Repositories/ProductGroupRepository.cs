using Microsoft.EntityFrameworkCore;
using P1.Domain.Entities;
using P1.Infrastructure.Contracts.Persistence.Command;
using P1.Infrastructure.Contracts.Persistence.Query;
using P1.Infrastructure.Persistence;

namespace P1.Infrastructure.Persistence.Repositories;

public class ProductGroupRepository : IProductGroupQueryRepository, IProductGroupCommandRepository
{
    private readonly AppDbContext _context;

    public ProductGroupRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<ProductGroup?> GetByIdAsync(int id, CancellationToken ct = default) =>
        _context.ProductGroups.FirstOrDefaultAsync(g => g.Id == id, ct);

    public Task<List<ProductGroup>> GetAllAsync(CancellationToken ct = default) =>
        _context.ProductGroups.AsNoTracking().OrderBy(g => g.Id).ToListAsync(ct);

    public async Task AddAsync(ProductGroup group, CancellationToken ct = default) =>
        await _context.ProductGroups.AddAsync(group, ct);

    public void Update(ProductGroup group) => _context.ProductGroups.Update(group);

    public void Remove(ProductGroup group) => _context.ProductGroups.Remove(group);


}
