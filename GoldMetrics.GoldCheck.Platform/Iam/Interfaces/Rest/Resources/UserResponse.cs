namespace GoldMetrics.GoldCheck.Platform.Iam.Interfaces.Rest.Resources;

public record UserResponse(int Id, string Username, string Email, string Role, string Status);

