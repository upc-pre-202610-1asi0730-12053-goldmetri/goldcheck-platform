namespace GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Domain.Model.ValueObjects;

public record ProductQRCode
{
    public ProductQRCode() => Value = string.Empty;

    public ProductQRCode(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("ProductQRCode cannot be empty.", nameof(value));
        Value = value;
    }

    public string Value { get; init; }
}