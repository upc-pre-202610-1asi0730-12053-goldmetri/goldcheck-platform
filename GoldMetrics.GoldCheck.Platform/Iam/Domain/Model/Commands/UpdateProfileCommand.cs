namespace GoldMetrics.GoldCheck.Platform.Iam.Domain.Model.Commands;

public record UpdateProfileCommand(string UserId, string Username, string Email);