using GoldMetrics.GoldCheck.Platform.Shared.Domain.Model.Events;

namespace GoldMetrics.GoldCheck.Platform.AssetMaintenance.Domain.Model.Events;

public record PreventiveMaintenanceScheduledEvent(string MachineryId, decimal EngineHours) : IEvent;