using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using P1.Domain.Entities;
using P1.Infrastructure.Contracts.Persistence.Command;
using P1.Infrastructure.Contracts.Persistence.Query;
using P1.Infrastructure.Persistence;

namespace P1.Infrastructure.Persistence.Repositories;

public class ProductRepository : IProductQueryRepository, IProductCommandRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<Product?> GetByIdAsync(int id, CancellationToken ct = default) =>
        _context.Products.Include(p => p.ProductGroup).FirstOrDefaultAsync(p => p.Id == id, ct);

    public Task<List<Product>> GetAllAsync(CancellationToken ct = default) =>
        _context.Products.AsNoTracking().Include(p => p.ProductGroup).OrderBy(p => p.Id).ToListAsync(ct);

    public async Task AddAsync(Product product, CancellationToken ct = default) =>
        await _context.Products.AddAsync(product, ct);

    public void Update(Product product) => _context.Products.Update(product);

    public void Remove(Product product) => _context.Products.Remove(product);

    public async Task<List<Product>> GetByGroupIdAsync(int groupId, CancellationToken ct = default)
    {
        return await _context.Products
            .Where(p => p.ProductGroupId == groupId)
            .ToListAsync(ct);
    }


    /// <summary>Reads from the SQL View vw_ProductsWithGroup.</summary>
    public Task<List<ProductWithGroupView>> GetProductsWithGroupAsync(CancellationToken ct = default) =>
        _context.ProductsWithGroupView.AsNoTracking().ToListAsync(ct);

    /// <summary>Calls the stored procedure sp_GetProductsByGroup via FromSqlInterpolated.</summary>
    public Task<List<ProductByGroupResult>> GetProductsByGroupAsync(int productGroupId, CancellationToken ct = default)
    {
        var groupIdParam = new SqlParameter("@ProductGroupId", productGroupId);

        return _context.ProductByGroupResults
            .FromSqlInterpolated($"EXEC sp_GetProductsByGroup @ProductGroupId = {groupIdParam}")
            .ToListAsync(ct);
    }
}
