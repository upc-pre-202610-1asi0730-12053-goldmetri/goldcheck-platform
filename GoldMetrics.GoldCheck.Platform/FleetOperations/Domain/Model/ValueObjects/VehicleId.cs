namespace GoldMetrics.GoldCheck.Platform.FleetOperations.Domain.Model.ValueObjects;

public record VehicleId
{
    public VehicleId() => Value = string.Empty;

    public VehicleId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("VehicleId cannot be empty.", nameof(value));
        Value = value;
    }

    public string Value { get; init; }
}
