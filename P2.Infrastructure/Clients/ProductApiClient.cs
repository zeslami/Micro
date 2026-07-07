using P2.Application.Contracts.DTOs.Products;
using P2.Infrastructure.Contracts.Clients;

namespace P2.Infrastructure.Clients;

public class ProductApiClient : ApiClientBase, IProductApiClient
{
    public ProductApiClient(HttpClient httpClient) : base(httpClient)
    {
    }

    public Task<List<ProductDto>> GetAllAsync(CancellationToken ct = default) =>
        GetAsync<List<ProductDto>>("api/products", ct);

    public Task<ProductDto> GetByIdAsync(int id, CancellationToken ct = default) =>
        GetAsync<ProductDto>($"api/products/{id}", ct);

    public Task<ProductDto> CreateAsync(CreateProductDto dto, CancellationToken ct = default) =>
        PostAsync<CreateProductDto, ProductDto>("api/products", dto, ct);

    public Task UpdateAsync(int id, UpdateProductDto dto, CancellationToken ct = default) =>
        PutAsync($"api/products/{id}", dto, ct);

    public Task DeleteAsync(int id, CancellationToken ct = default) =>
        DeleteAsync($"api/products/{id}", ct);

    public Task<List<ProductDto>> GetByGroupAsync(int productGroupId, CancellationToken ct = default) =>
        GetAsync<List<ProductDto>>($"api/products/by-group/{productGroupId}", ct);
}
