using GoldMetrics.GoldCheck.Platform.Shared.Domain.Model.Events;

namespace GoldMetrics.GoldCheck.Platform.AssetMaintenance.Domain.Model.Events;

public record ComponentDischargedEvent(string MachineryId, string ComponentId, string Reason) : IEvent;