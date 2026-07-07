using P1.Application.Contracts.Common;
using P1.Application.Contracts.DTOs;
using P1.Application.Services.Base;
using P1.Application.Contracts.Services;
using P1.Domain.Entities;
using P1.Infrastructure.Contracts.Persistence;
using P1.Infrastructure.Contracts.Security;
using P1.Infrastructure.Contracts.Persistence.Command;
using P1.Infrastructure.Contracts.Persistence.Query; // مسیر واقعی IPasswordHasher شما

namespace P1.Application.Services;

public class UserService : GenericService<User, int, UserDto, CreateUserDto, UpdateUserDto>, IUserService
{
    private readonly IUserQueryRepository _queryRepository;
    private readonly IUserCommandRepository _commandRepository;
    private readonly IPasswordHasher _passwordHasher;

    public UserService(
        IUnitOfWork unitOfWork,
        IUserQueryRepository queryRepository,
        IUserCommandRepository commandRepository,
        IPasswordHasher passwordHasher)
        : base(
            unitOfWork,
            entity => UserDto.FromEntity(entity),
            createDto => createDto.ToEntity(passwordHasher.HashPassword(createDto.Password)),
            (updateDto, entity) => updateDto.UpdateEntity(entity))
    {
        _queryRepository = queryRepository;
        _commandRepository = commandRepository;
        _passwordHasher = passwordHasher;
    }

    protected override async Task<IEnumerable<User>> GetEntitiesAsync(CancellationToken cancellationToken)
    {
        return await _queryRepository.GetAllAsync(cancellationToken);
    }

    protected override async Task<User?> GetEntityByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _queryRepository.GetByIdAsync(id, cancellationToken);
    }

    protected override async Task AddEntityAsync(User entity, CancellationToken cancellationToken)
    {
        await _commandRepository.AddAsync(entity, cancellationToken);
    }

    protected override void DeleteEntity(User entity)
    {
        _commandRepository.Remove(entity);
    }

    public async Task<Result<UserDto>> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        var user = await _queryRepository.GetByUsernameAsync(username, cancellationToken);
        if (user == null)
            return Result.Failure<UserDto>("کاربر مورد نظر یافت نشد.");

        return Result.Success(UserDto.FromEntity(user));
    }
}
