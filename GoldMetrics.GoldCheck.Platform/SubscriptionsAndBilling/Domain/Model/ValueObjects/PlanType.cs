namespace GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Domain.Model.ValueObjects;
public record PlanType
{
    private static readonly string[] AllowedValues = ["Free", "Basic", "Professional", "Enterprise"];
    public PlanType() => Value = string.Empty;
    public PlanType(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("PlanType cannot be empty.", nameof(value));
        if (!AllowedValues.Contains(value))
            throw new ArgumentException($"PlanType must be one of: {string.Join(", ", AllowedValues)}.", nameof(value));
        Value = value;
    }
    public string Value { get; init; }
}