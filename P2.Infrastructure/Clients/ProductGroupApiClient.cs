using P2.Application.Contracts.DTOs.ProductGroups;
using P2.Infrastructure.Contracts.Clients;

namespace P2.Infrastructure.Clients;

public class ProductGroupApiClient : ApiClientBase, IProductGroupApiClient
{
    public ProductGroupApiClient(HttpClient httpClient) : base(httpClient)
    {
    }

    public Task<List<ProductGroupDto>> GetAllAsync(CancellationToken ct = default) =>
        GetAsync<List<ProductGroupDto>>("api/productgroups", ct);

    public Task<ProductGroupDto> GetByIdAsync(int id, CancellationToken ct = default) =>
        GetAsync<ProductGroupDto>($"api/productgroups/{id}", ct);

    public Task<ProductGroupDto> CreateAsync(CreateProductGroupDto dto, CancellationToken ct = default) =>
        PostAsync<CreateProductGroupDto, ProductGroupDto>("api/productgroups", dto, ct);

    public Task UpdateAsync(int id, UpdateProductGroupDto dto, CancellationToken ct = default) =>
        PutAsync($"api/productgroups/{id}", dto, ct);

    public Task DeleteAsync(int id, CancellationToken ct = default) =>
        DeleteAsync($"api/productgroups/{id}", ct);
}
