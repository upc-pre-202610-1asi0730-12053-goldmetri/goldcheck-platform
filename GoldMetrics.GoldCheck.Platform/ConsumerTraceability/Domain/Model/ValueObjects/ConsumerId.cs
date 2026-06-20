namespace GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Domain.Model.ValueObjects;

public record ConsumerId
{
    public ConsumerId() => Value = string.Empty;

    public ConsumerId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("ConsumerId cannot be empty.", nameof(value));
        Value = value;
    }

    public string Value { get; init; }
}