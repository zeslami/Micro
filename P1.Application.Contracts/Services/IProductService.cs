using P1.Application.Contracts.Common;
using P1.Application.Contracts.DTOs;
using P1.Application.Contracts.Services.Base;

namespace P1.Application.Contracts.Services;

public interface IProductService : IGenericService<int, ProductDto, CreateProductDto, UpdateProductDto>
{
    // متد اختصاصی برای بیزنس محصولات که خارج از CRUD جنریک است
    Task<Result<IEnumerable<ProductDto>>> GetProductsByGroupIdAsync(int groupId, CancellationToken cancellationToken = default);
}
