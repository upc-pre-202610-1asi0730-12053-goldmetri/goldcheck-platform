namespace GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Domain.Model.ValueObjects;
public record Amount
{
    public Amount() => Value = 0m;
    public Amount(decimal value)
    {
        if (value < 0)
            throw new ArgumentException("Amount must be greater than or equal to 0.", nameof(value));
        Value = value;
    }
    public decimal Value { get; init; }
}