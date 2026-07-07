namespace P1.Application.Contracts.DTOs.Auth;

public class LoginRequestDto
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}
