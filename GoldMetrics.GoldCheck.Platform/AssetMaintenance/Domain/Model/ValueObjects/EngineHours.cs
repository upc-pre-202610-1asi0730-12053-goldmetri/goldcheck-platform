namespace GoldMetrics.GoldCheck.Platform.AssetMaintenance.Domain.Model.ValueObjects;

public record EngineHours
{
    public EngineHours() => Hours = 0;
    public EngineHours(decimal hours)
    {
        if (hours < 0)
            throw new ArgumentException("EngineHours cannot be negative.", nameof(hours));
        Hours = hours;
    }
    public decimal Hours { get; init; }
}