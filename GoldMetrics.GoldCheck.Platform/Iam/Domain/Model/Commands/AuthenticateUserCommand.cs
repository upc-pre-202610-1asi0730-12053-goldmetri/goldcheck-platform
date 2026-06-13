namespace GoldMetrics.GoldCheck.Platform.Iam.Domain.Model.Commands;

public record AuthenticateUserCommand(string Username, string Password);

