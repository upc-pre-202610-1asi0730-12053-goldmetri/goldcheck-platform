namespace GoldMetrics.GoldCheck.Platform.JewelryInventory.Domain.Model.ValueObjects;

public record JewelerId
{
    public JewelerId() => Value = string.Empty;

    public JewelerId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("JewelerId cannot be empty.", nameof(value));
        Value = value;
    }

    public string Value { get; init; }
}
