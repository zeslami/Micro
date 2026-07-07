using P1.Application.Contracts.Common;
using P1.Application.Contracts.DTOs;
using P1.Application.Contracts.Services.Base;

namespace P1.Application.Contracts.Services;

public interface IUserService : IGenericService<int, UserDto, CreateUserDto, UpdateUserDto>
{
    Task<Result<UserDto>> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);
}
