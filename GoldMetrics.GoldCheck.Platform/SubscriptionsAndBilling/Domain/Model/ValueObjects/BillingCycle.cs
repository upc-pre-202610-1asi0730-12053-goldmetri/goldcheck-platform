namespace GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Domain.Model.ValueObjects;
public record BillingCycle
{
    private static readonly string[] AllowedValues = ["Monthly", "Annual"];
    public BillingCycle() => Value = string.Empty;
    public BillingCycle(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("BillingCycle cannot be empty.", nameof(value));
        if (!AllowedValues.Contains(value))
            throw new ArgumentException($"BillingCycle must be one of: {string.Join(", ", AllowedValues)}.", nameof(value));
        Value = value;
    }
    public string Value { get; init; }
}