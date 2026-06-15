namespace GoldMetrics.GoldCheck.Platform.ReportingNotifications.Domain.Model.ValueObjects;

public record ReportStatus
{
    private static readonly string[] AllowedValues = ["Requested", "DataLoaded", "Generated", "ExportationRequested", "Exported"];
    public ReportStatus() => Value = string.Empty;
    public ReportStatus(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("ReportStatus cannot be empty.", nameof(value));
        if (!AllowedValues.Contains(value))
            throw new ArgumentException($"ReportStatus must be one of: {string.Join(", ", AllowedValues)}.", nameof(value));
        Value = value;
    }
    public string Value { get; init; }
}