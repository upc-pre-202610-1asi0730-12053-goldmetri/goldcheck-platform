namespace GoldMetrics.GoldCheck.Platform.Iam.Domain.Model.Commands;

public record RegisterUserCommand(string Username, string Password, string Email, string Role);

