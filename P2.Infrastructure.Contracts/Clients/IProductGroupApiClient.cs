using P2.Application.Contracts.DTOs.ProductGroups;

namespace P2.Infrastructure.Contracts.Clients;

/// <summary>Forwards requests to P1's ProductGroupsController.</summary>
public interface IProductGroupApiClient
{
    Task<List<ProductGroupDto>> GetAllAsync(CancellationToken ct = default);
    Task<ProductGroupDto> GetByIdAsync(int id, CancellationToken ct = default);
    Task<ProductGroupDto> CreateAsync(CreateProductGroupDto dto, CancellationToken ct = default);
    Task UpdateAsync(int id, UpdateProductGroupDto dto, CancellationToken ct = default);
    Task DeleteAsync(int id, CancellationToken ct = default);
}
