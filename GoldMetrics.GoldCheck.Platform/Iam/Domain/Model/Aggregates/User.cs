using GoldMetrics.GoldCheck.Platform.Iam.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.Iam.Domain.Model.ValueObjects;

namespace GoldMetrics.GoldCheck.Platform.Iam.Domain.Model.Aggregates;

public partial class User
{
    public int Id { get; private set; }
    public Username Username { get; private set; } = new();
    public HashedPassword HashedPassword { get; private set; } = new();
    public Email Email { get; private set; } = new();
    public UserRole Role { get; private set; } = new();
    public string Status { get; private set; } = string.Empty;

    public User() { }

    public User(RegisterUserCommand command)
    {
        Username = new Username(command.Username);
        HashedPassword = new HashedPassword(BCrypt.Net.BCrypt.HashPassword(command.Password));
        Email = new Email(command.Email);
        Role = new UserRole(command.Role);
        Status = "UserRegistered";
    }
}