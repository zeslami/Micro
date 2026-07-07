using P2.Application.Contracts.DTOs.Products;

namespace P2.Infrastructure.Contracts.Clients;

/// <summary>Forwards requests to P1's ProductsController.</summary>
public interface IProductApiClient
{
    Task<List<ProductDto>> GetAllAsync(CancellationToken ct = default);
    Task<ProductDto> GetByIdAsync(int id, CancellationToken ct = default);
    Task<ProductDto> CreateAsync(CreateProductDto dto, CancellationToken ct = default);
    Task UpdateAsync(int id, UpdateProductDto dto, CancellationToken ct = default);
    Task DeleteAsync(int id, CancellationToken ct = default);
    Task<List<ProductDto>> GetByGroupAsync(int productGroupId, CancellationToken ct = default);
}
