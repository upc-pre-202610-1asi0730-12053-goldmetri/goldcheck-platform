namespace GoldMetrics.GoldCheck.Platform.FleetOperations.Domain.Model.ValueObjects;

public record CycleId
{
    public CycleId() => Value = string.Empty;

    public CycleId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("CycleId cannot be empty.", nameof(value));
        Value = value;
    }

    public string Value { get; init; }
}
