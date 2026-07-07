using P1.Application.Contracts.DTOs.Auth;
using P1.Application.Contracts.Services;
using P1.Domain.Entities;
using P1.Infrastructure.Contracts.Persistence;
using P1.Infrastructure.Contracts.Persistence.Command;
using P1.Infrastructure.Contracts.Persistence.Query;
using P1.Infrastructure.Contracts.Security;

namespace P1.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserQueryRepository _userQuery;
    private readonly IUserCommandRepository _userCommand;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtService _jwtService;

    public AuthService(
        IUserQueryRepository userQuery,
        IUserCommandRepository userCommand,
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher,
        IJwtService jwtService)
    {
        _userQuery = userQuery;
        _userCommand = userCommand;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto dto, CancellationToken ct = default)
    {
        // چک تکراری نبودن یوزرنیم
        if (await _userQuery.ExistsByUsernameAsync(dto.Username, ct))
            throw new InvalidOperationException($"Username '{dto.Username}' is already taken.");

        await _unitOfWork.BeginTransactionAsync(ct);
        try
        {
            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = _passwordHasher.HashPassword(dto.Password),
                Role = "User",
                CreatedAt = DateTime.UtcNow
            };

            await _userCommand.AddAsync(user, ct);
            await _unitOfWork.CommitTransactionAsync(ct);

            var (token, expiresAtUtc) = _jwtService.GenerateToken(user);

            return new AuthResponseDto
            {
                UserId = user.Id,
                Username = user.Username,
                Role = user.Role,
                Token = token,
                ExpiresAtUtc = expiresAtUtc
            };
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync(ct);
            throw;
        }
    }

    public async Task<AuthResponseDto> LoginAsync(LoginRequestDto dto, CancellationToken ct = default)
    {
        var user = await _userQuery.GetByUsernameAsync(dto.Username, ct);

        // پیام یکسان برای هر دو حالت — Username Enumeration Attack
        if (user is null || !_passwordHasher.VerifyPassword(dto.Password, user.PasswordHash))
            throw new UnauthorizedAccessException("Invalid username or password.");

        var (token, expiresAtUtc) = _jwtService.GenerateToken(user);

        return new AuthResponseDto
        {
            UserId = user.Id,
            Username = user.Username,
            Role = user.Role,
            Token = token,
            ExpiresAtUtc = expiresAtUtc
        };
    }
}
