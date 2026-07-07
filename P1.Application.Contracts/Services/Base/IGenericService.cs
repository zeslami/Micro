using P1.Application.Contracts.Common;

namespace P1.Application.Contracts.Services.Base;

public interface IGenericService<TKey, TDto, TCreateDto, TUpdateDto>
    where TDto : class
    where TCreateDto : class
    where TUpdateDto : class
{
    Task<Result<IEnumerable<TDto>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<TDto>> GetByIdAsync(TKey id, CancellationToken cancellationToken = default);
    Task<Result<TDto>> CreateAsync(TCreateDto createDto, CancellationToken cancellationToken = default);
    Task<Result> UpdateAsync(TKey id, TUpdateDto updateDto, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(TKey id, CancellationToken cancellationToken = default);
}
