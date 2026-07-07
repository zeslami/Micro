using P1.Application.Contracts.Common;
using P1.Application.Contracts.DTOs;
using P1.Application.Services.Base;
using P1.Application.Contracts.Services;
using P1.Domain.Entities;
using P1.Infrastructure.Contracts.Persistence;
using P1.Infrastructure.Contracts.Persistence.Command;
using P1.Infrastructure.Contracts.Persistence.Query;

namespace P1.Application.Services;

public class ProductService : GenericService<Product, int, ProductDto, CreateProductDto, UpdateProductDto>, IProductService
{
    private readonly IProductQueryRepository _queryRepository;
    private readonly IProductCommandRepository _commandRepository;

    public ProductService(
        IUnitOfWork unitOfWork,
        IProductQueryRepository queryRepository,
        IProductCommandRepository commandRepository)
        : base(
            unitOfWork,
            entity => ProductDto.FromEntity(entity),
            createDto => createDto.ToEntity(),
            (updateDto, entity) => updateDto.UpdateEntity(entity))
    {
        _queryRepository = queryRepository ?? throw new ArgumentNullException(nameof(queryRepository));
        _commandRepository = commandRepository ?? throw new ArgumentNullException(nameof(commandRepository));
    }

    protected override async Task<IEnumerable<Product>> GetEntitiesAsync(CancellationToken cancellationToken)
    {
        // در صورت نیاز به شامل کردن ارتباطات یا متدهای خاص ریپازیتوری
        return await _queryRepository.GetAllAsync(cancellationToken);
    }

    protected override async Task<Product?> GetEntityByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _queryRepository.GetByIdAsync(id, cancellationToken);
    }

    protected override async Task AddEntityAsync(Product entity, CancellationToken cancellationToken)
    {
        await _commandRepository.AddAsync(entity, cancellationToken);
    }

    protected override void DeleteEntity(Product entity)
    {
        _commandRepository.Remove(entity);
    }

    // پیاده‌سازی متد اختصاصی
    public async Task<Result<IEnumerable<ProductDto>>> GetProductsByGroupIdAsync(int groupId, CancellationToken cancellationToken = default)
    {
        var products = await _queryRepository.GetByGroupIdAsync(groupId, cancellationToken);
        if (products == null || !products.Any())
            return Result.Failure<IEnumerable<ProductDto>>("محصولی در این گروه یافت نشد.");

        var dtos = products.Select(ProductDto.FromEntity);
        return Result.Success(dtos);
    }
}
