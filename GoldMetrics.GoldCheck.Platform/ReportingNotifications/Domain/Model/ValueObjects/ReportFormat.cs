namespace GoldMetrics.GoldCheck.Platform.ReportingNotifications.Domain.Model.ValueObjects;

public record ReportFormat
{
    private static readonly string[] AllowedValues = ["PDF", "Excel", "CSV"];
    public ReportFormat() => Value = string.Empty;
    public ReportFormat(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("ReportFormat cannot be empty.", nameof(value));
        if (!AllowedValues.Contains(value))
            throw new ArgumentException($"ReportFormat must be one of: {string.Join(", ", AllowedValues)}.", nameof(value));
        Value = value;
    }
    public string Value { get; init; }
}