namespace GoldMetrics.GoldCheck.Platform.Iam.Domain.Model.Errors;

public static class IamErrors
{
    public static string KeyFor(IamError error) => $"IamError.{error}";
}