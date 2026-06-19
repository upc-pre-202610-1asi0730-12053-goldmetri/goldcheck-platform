namespace GoldMetrics.GoldCheck.Platform.FleetOperations.Domain.Model.ValueObjects;

public record Payload
{
    public Payload() => Tons = 0;

    public Payload(decimal tons)
    {
        if (tons <= 0)
            throw new ArgumentException("Payload must be greater than 0.", nameof(tons));
        Tons = tons;
    }

    public decimal Tons { get; init; }
}
