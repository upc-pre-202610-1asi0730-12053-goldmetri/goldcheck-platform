namespace GoldMetrics.GoldCheck.Platform.ReportingNotifications.Domain.Model.ValueObjects;

public record ReportId
{
    public ReportId() => Value = string.Empty;
    public ReportId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("ReportId cannot be empty.", nameof(value));
        Value = value;
    }
    public string Value { get; init; }
}
