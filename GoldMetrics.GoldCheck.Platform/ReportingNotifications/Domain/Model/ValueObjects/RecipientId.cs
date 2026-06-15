namespace GoldMetrics.GoldCheck.Platform.ReportingNotifications.Domain.Model.ValueObjects;

public record RecipientId
{
    public RecipientId() => Value = string.Empty;
    public RecipientId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("RecipientId cannot be empty.", nameof(value));
        Value = value;
    }
    public string Value { get; init; }
}