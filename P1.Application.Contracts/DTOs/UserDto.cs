using P1.Domain.Entities;

namespace P1.Application.Contracts.DTOs;

public class UserDto
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;

    public static UserDto FromEntity(User user) => new()
    {
        Id = user.Id,
        Username = user.Username,
        Email = user.Email,
        Role = user.Role
    };
}

public class CreateUserDto
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Role { get; set; } = "User";

    public User ToEntity(string hashedPassword) => new()
    {
        Username = this.Username,
        Email = this.Email,
        PasswordHash = hashedPassword,
        Role = this.Role
    };
}

public class UpdateUserDto
{
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;

    public void UpdateEntity(User user)
    {
        user.Email = this.Email;
        user.Role = this.Role;
    }
}
