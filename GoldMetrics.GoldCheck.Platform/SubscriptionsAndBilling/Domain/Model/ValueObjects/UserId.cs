namespace GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Domain.Model.ValueObjects;
public record UserId
{
    public UserId() => Value = string.Empty;
    public UserId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("UserId cannot be empty.", nameof(value));
        Value = value;
    }
    public string Value { get; init; }
}