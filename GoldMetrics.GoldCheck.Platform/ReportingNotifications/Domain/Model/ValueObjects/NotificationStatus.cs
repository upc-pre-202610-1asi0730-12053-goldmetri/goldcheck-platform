namespace GoldMetrics.GoldCheck.Platform.ReportingNotifications.Domain.Model.ValueObjects;

public record NotificationStatus
{
    private static readonly string[] AllowedValues = ["Requested"];
    public NotificationStatus() => Value = string.Empty;
    public NotificationStatus(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("NotificationStatus cannot be empty.", nameof(value));
        if (!AllowedValues.Contains(value))
            throw new ArgumentException($"NotificationStatus must be one of: {string.Join(", ", AllowedValues)}.", nameof(value));
        Value = value;
    }
    public string Value { get; init; }
}