namespace GoldMetrics.GoldCheck.Platform.Analytics.Domain.Model.ValueObjects;

public record RouteId
{
    public RouteId() => Value = string.Empty;
    public RouteId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("RouteId cannot be empty.", nameof(value));
        Value = value;
    }
    public string Value { get; init; }
}