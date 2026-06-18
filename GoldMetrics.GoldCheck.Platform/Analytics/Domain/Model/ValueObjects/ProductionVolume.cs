namespace GoldMetrics.GoldCheck.Platform.Analytics.Domain.Model.ValueObjects;

public record ProductionVolume
{
    public ProductionVolume() => Tons = 0;
    public ProductionVolume(decimal tons)
    {
        if (tons < 0)
            throw new ArgumentException("ProductionVolume cannot be negative.", nameof(tons));
        Tons = tons;
    }
    public decimal Tons { get; init; }
}