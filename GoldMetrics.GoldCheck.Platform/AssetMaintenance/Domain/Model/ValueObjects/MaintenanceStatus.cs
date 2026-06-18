namespace GoldMetrics.GoldCheck.Platform.AssetMaintenance.Domain.Model.ValueObjects;

public record MaintenanceStatus
{
    private static readonly string[] AllowedValues = ["Active", "UnderMaintenance", "Discharged"];
    public MaintenanceStatus() => Value = string.Empty;
    public MaintenanceStatus(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("MaintenanceStatus cannot be empty.", nameof(value));
        if (!AllowedValues.Contains(value))
            throw new ArgumentException($"MaintenanceStatus must be one of: {string.Join(", ", AllowedValues)}.", nameof(value));
        Value = value;
    }
    public string Value { get; init; }
}