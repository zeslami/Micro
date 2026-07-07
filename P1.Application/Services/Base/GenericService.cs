using P1.Application.Contracts.Common;
using P1.Application.Contracts.Services.Base;
using P1.Infrastructure.Contracts.Persistence;

namespace P1.Application.Services.Base;

public class GenericService<TEntity, TKey, TDto, TCreateDto, TUpdateDto> : IGenericService<TKey, TDto, TCreateDto, TUpdateDto>
    where TEntity : class
    where TDto : class
    where TCreateDto : class
    where TUpdateDto : class
{
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly Func<TEntity, TDto> _toDto;
    protected readonly Func<TCreateDto, TEntity> _toEntityFromCreate;
    protected readonly Action<TUpdateDto, TEntity> _updateEntityFromDto;

    public GenericService(
        IUnitOfWork unitOfWork,
        Func<TEntity, TDto> toDto,
        Func<TCreateDto, TEntity> toEntityFromCreate,
        Action<TUpdateDto, TEntity> updateEntityFromDto)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _toDto = toDto ?? throw new ArgumentNullException(nameof(toDto));
        _toEntityFromCreate = toEntityFromCreate ?? throw new ArgumentNullException(nameof(toEntityFromCreate));
        _updateEntityFromDto = updateEntityFromDto ?? throw new ArgumentNullException(nameof(updateEntityFromDto));
    }

    // متدهای کمکی جهت دسترسی به ریپازیتوری‌های اختصاصی در سرویس‌های فرزند
    protected virtual Task<IEnumerable<TEntity>> GetEntitiesAsync(CancellationToken cancellationToken) => throw new NotImplementedException();
    protected virtual Task<TEntity?> GetEntityByIdAsync(TKey id, CancellationToken cancellationToken) => throw new NotImplementedException();
    protected virtual Task AddEntityAsync(TEntity entity, CancellationToken cancellationToken) => throw new NotImplementedException();
    protected virtual void DeleteEntity(TEntity entity) => throw new NotImplementedException();

    public virtual async Task<Result<IEnumerable<TDto>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var entities = await GetEntitiesAsync(cancellationToken);
        var dtos = entities.Select(_toDto);
        return Result.Success(dtos);
    }

    public virtual async Task<Result<TDto>> GetByIdAsync(TKey id, CancellationToken cancellationToken = default)
    {
        var entity = await GetEntityByIdAsync(id, cancellationToken);
        if (entity == null)
            return Result.Failure<TDto>("رکورد مورد نظر یافت نشد.");

        return Result.Success(_toDto(entity));
    }

    public virtual async Task<Result<TDto>> CreateAsync(TCreateDto createDto, CancellationToken cancellationToken = default)
    {
        if (createDto == null)
            return Result.Failure<TDto>("اطلاعات ارسالی نامعتبر است.");

        var entity = _toEntityFromCreate(createDto);

        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            await AddEntityAsync(entity, cancellationToken);
            await _unitOfWork.CommitTransactionAsync(cancellationToken);
            return Result.Success(_toDto(entity));
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            return Result.Failure<TDto>($"خطا در ثبت اطلاعات: {ex.Message}");
        }
    }

    public virtual async Task<Result> UpdateAsync(TKey id, TUpdateDto updateDto, CancellationToken cancellationToken = default)
    {
        if (updateDto == null)
            return Result.Failure("اطلاعات ارسالی نامعتبر است.");

        var entity = await GetEntityByIdAsync(id, cancellationToken);
        if (entity == null)
            return Result.Failure("رکورد مورد نظر یافت نشد.");

        _updateEntityFromDto(updateDto, entity);

        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            await _unitOfWork.CommitTransactionAsync(cancellationToken);
            return Result.Success();
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            return Result.Failure($"خطا در بروزرسانی اطلاعات: {ex.Message}");
        }
    }

    public virtual async Task<Result> DeleteAsync(TKey id, CancellationToken cancellationToken = default)
    {
        var entity = await GetEntityByIdAsync(id, cancellationToken);
        if (entity == null)
            return Result.Failure("رکورد مورد نظر یافت نشد.");

        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            DeleteEntity(entity);
            await _unitOfWork.CommitTransactionAsync(cancellationToken);
            return Result.Success();
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            return Result.Failure($"خطا در حذف اطلاعات: {ex.Message}");
        }
    }
}
