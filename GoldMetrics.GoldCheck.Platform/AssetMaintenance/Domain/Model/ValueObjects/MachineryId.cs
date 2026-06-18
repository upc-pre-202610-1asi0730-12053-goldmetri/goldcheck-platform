namespace GoldMetrics.GoldCheck.Platform.AssetMaintenance.Domain.Model.ValueObjects;

public record MachineryId
{
    public MachineryId() => Value = string.Empty;
    public MachineryId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("MachineryId cannot be empty.", nameof(value));
        Value = value;
    }
    public string Value { get; init; }
}