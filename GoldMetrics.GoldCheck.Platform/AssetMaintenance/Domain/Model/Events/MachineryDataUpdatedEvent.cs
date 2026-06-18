using GoldMetrics.GoldCheck.Platform.Shared.Domain.Model.Events;

namespace GoldMetrics.GoldCheck.Platform.AssetMaintenance.Domain.Model.Events;

public record MachineryDataUpdatedEvent(string MachineryId, decimal CurrentEngineHours) : IEvent;