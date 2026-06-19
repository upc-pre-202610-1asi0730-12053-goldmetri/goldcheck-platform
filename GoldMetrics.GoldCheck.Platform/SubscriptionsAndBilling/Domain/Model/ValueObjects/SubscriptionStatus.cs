namespace GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Domain.Model.ValueObjects;
public record SubscriptionStatus
{
    private static readonly string[] AllowedValues = ["Active", "DowngradeRequested", "Restricted"];
    public SubscriptionStatus() => Value = string.Empty;
    public SubscriptionStatus(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("SubscriptionStatus cannot be empty.", nameof(value));
        if (!AllowedValues.Contains(value))
            throw new ArgumentException($"SubscriptionStatus must be one of: {string.Join(", ", AllowedValues)}.", nameof(value));
        Value = value;
    }
    public string Value { get; init; }
}