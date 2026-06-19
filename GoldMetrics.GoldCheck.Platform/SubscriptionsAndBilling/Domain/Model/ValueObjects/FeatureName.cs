namespace GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Domain.Model.ValueObjects;
public record FeatureName
{
    public FeatureName() => Value = string.Empty;
    public FeatureName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("FeatureName cannot be empty.", nameof(value));
        Value = value;
    }
    public string Value { get; init; }
}