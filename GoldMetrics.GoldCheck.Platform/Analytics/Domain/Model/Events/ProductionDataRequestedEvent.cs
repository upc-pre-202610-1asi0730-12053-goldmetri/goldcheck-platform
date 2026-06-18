using GoldMetrics.GoldCheck.Platform.Shared.Domain.Model.Events;

namespace GoldMetrics.GoldCheck.Platform.Analytics.Domain.Model.Events;

public record ProductionDataRequestedEvent(string SupervisorId, DateTimeOffset Start, DateTimeOffset End) : IEvent;