namespace GoldMetrics.GoldCheck.Platform.JewelryInventory.Domain.Model.ValueObjects;

public record MaterialId
{
    public MaterialId() => Value = string.Empty;

    public MaterialId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("MaterialId cannot be empty.", nameof(value));
        Value = value;
    }

    public string Value { get; init; }
}
