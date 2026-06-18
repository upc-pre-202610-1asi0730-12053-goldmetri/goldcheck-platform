using GoldMetrics.GoldCheck.Platform.Shared.Domain.Model.Events;

namespace GoldMetrics.GoldCheck.Platform.Analytics.Domain.Model.Events;

public record ProductionDataValidatedEvent(string MaterialId) : IEvent;