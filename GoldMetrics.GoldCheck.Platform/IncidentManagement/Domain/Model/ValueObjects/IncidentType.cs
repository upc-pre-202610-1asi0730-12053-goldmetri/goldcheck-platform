namespace GoldMetrics.GoldCheck.Platform.IncidentManagement.Domain.Model.ValueObjects;

public record IncidentType
{
    private static readonly string[] AllowedValues = ["Fatigue", "Smoke", "Accident"];

    public IncidentType() => Value = string.Empty;

    public IncidentType(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("IncidentType cannot be empty.", nameof(value));
        if (!AllowedValues.Contains(value))
            throw new ArgumentException($"IncidentType must be one of: {string.Join(", ", AllowedValues)}.", nameof(value));
        Value = value;
    }

    public string Value { get; init; }
}