using P1.Application.Contracts.DTOs;
using P1.Application.Services.Base;
using P1.Application.Contracts.Services;
using P1.Domain.Entities;
using P1.Infrastructure.Contracts.Persistence;
using P1.Infrastructure.Contracts.Persistence.Command;
using P1.Infrastructure.Contracts.Persistence.Query;

namespace P1.Application.Services;

public class ProductGroupService : GenericService<ProductGroup, int, ProductGroupDto, CreateProductGroupDto, UpdateProductGroupDto>, IProductGroupService
{
    private readonly IProductGroupQueryRepository _queryRepository;
    private readonly IProductGroupCommandRepository _commandRepository;

    public ProductGroupService(
        IUnitOfWork unitOfWork,
        IProductGroupQueryRepository queryRepository,
        IProductGroupCommandRepository commandRepository)
        : base(
            unitOfWork,
            entity => ProductGroupDto.FromEntity(entity),
            createDto => createDto.ToEntity(),
            (updateDto, entity) => updateDto.UpdateEntity(entity))
    {
        _queryRepository = queryRepository;
        _commandRepository = commandRepository;
    }

    protected override async Task<IEnumerable<ProductGroup>> GetEntitiesAsync(CancellationToken cancellationToken)
    {
        return await _queryRepository.GetAllAsync(cancellationToken);
    }

    protected override async Task<ProductGroup?> GetEntityByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _queryRepository.GetByIdAsync(id, cancellationToken);
    }

    protected override async Task AddEntityAsync(ProductGroup entity, CancellationToken cancellationToken)
    {
        await _commandRepository.AddAsync(entity, cancellationToken);
    }

    protected override void DeleteEntity(ProductGroup entity)
    {
        _commandRepository.Remove(entity);
    }
}
