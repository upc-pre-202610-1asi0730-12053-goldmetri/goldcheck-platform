namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.ValueObjects;

public record Pressure
{
    public Pressure() => Bar = 0m;

    public Pressure(decimal bar)
    {
        if (bar < 0m)
            throw new ArgumentException(
                "Pressure must be greater than or equal to 0 bar.", nameof(bar));
        Bar = bar;
    }

    public decimal Bar { get; init; }
}