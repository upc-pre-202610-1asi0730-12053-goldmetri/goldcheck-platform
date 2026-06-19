namespace GoldMetrics.GoldCheck.Platform.FleetOperations.Domain.Model.ValueObjects;

public record OperatorId
{
    public OperatorId() => Value = string.Empty;

    public OperatorId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("OperatorId cannot be empty.", nameof(value));
        Value = value;
    }

    public string Value { get; init; }
}
