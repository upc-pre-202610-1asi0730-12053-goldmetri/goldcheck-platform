namespace GoldMetrics.GoldCheck.Platform.Analytics.Domain.Model.ValueObjects;

public record RouteStatus
{
    private static readonly string[] AllowedValues = ["InProgress", "Completed", "Delayed"];
    public RouteStatus() => Value = string.Empty;
    public RouteStatus(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("RouteStatus cannot be empty.", nameof(value));
        if (!AllowedValues.Contains(value))
            throw new ArgumentException($"RouteStatus must be one of: {string.Join(", ", AllowedValues)}.", nameof(value));
        Value = value;
    }
    public string Value { get; init; }
}