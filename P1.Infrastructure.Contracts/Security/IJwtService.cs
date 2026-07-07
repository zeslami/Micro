using P1.Domain.Entities;

namespace P1.Infrastructure.Contracts.Security;

public interface IJwtService
{
    (string Token, DateTime ExpiresAtUtc) GenerateToken(User user);
}
