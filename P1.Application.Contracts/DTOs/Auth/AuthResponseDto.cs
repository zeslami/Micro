namespace P1.Application.Contracts.DTOs.Auth;

public class AuthResponseDto
{
    public int UserId { get; set; }
    public string Username { get; set; } = null!;
    public string Role { get; set; } = null!;
    public string Token { get; set; } = null!;
    public DateTime ExpiresAtUtc { get; set; }
}
