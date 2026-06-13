namespace GoldMetrics.GoldCheck.Platform.Iam.Interfaces.Rest.Resources;

public record RegisterUserResource(string Username, string Password, string Email, string Role);

