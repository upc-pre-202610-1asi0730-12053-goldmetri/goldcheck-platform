namespace GoldMetrics.GoldCheck.Platform.AssetMaintenance.Domain.Model.ValueObjects;

public record ComponentId
{
    public ComponentId() => Value = string.Empty;
    public ComponentId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("ComponentId cannot be empty.", nameof(value));
        Value = value;
    }
    public string Value { get; init; }
}