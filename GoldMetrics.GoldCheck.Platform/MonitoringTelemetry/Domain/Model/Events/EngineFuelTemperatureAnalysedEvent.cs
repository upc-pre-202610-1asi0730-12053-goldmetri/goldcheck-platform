using GoldMetrics.GoldCheck.Platform.Shared.Domain.Model.Events;

namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Events;

public record EngineFuelTemperatureAnalysedEvent(string AssetId, decimal FuelCelsius) : IEvent;