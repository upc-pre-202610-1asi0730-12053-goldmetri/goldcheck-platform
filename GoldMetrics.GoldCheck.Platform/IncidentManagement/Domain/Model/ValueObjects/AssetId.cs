namespace GoldMetrics.GoldCheck.Platform.IncidentManagement.Domain.Model.ValueObjects;

public record AssetId
{
    public AssetId() => Value = string.Empty;

    public AssetId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("AssetId cannot be empty.", nameof(value));
        Value = value;
    }

    public string Value { get; init; }
}