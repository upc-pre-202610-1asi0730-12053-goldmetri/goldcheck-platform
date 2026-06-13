namespace GoldMetrics.GoldCheck.Platform.ReportingNotifications.Domain.Model.ValueObjects;

public record ReportStatus
{
    private static readonly string[] AllowedValues = ["Requested", "DataLoaded"];

}