using GoldMetrics.GoldCheck.Platform.Shared.Domain.Model.Events;

namespace GoldMetrics.GoldCheck.Platform.FleetOperations.Domain.Model.Events;

public record MaterialLoadedEvent(int CycleId, decimal PayloadTons) : IEvent;
