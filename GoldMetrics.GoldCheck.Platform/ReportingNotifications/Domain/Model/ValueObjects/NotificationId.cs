namespace GoldMetrics.GoldCheck.Platform.ReportingNotifications.Domain.Model.ValueObjects;

public record NotificationId
{
    public NotificationId() => Value = string.Empty;
    public NotificationId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("NotificationId cannot be empty.", nameof(value));
        Value = value;
    }
    public string Value { get; init; }
}
