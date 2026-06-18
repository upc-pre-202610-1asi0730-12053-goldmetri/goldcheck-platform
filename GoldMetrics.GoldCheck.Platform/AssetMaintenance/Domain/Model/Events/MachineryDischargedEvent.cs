using GoldMetrics.GoldCheck.Platform.Shared.Domain.Model.Events;

namespace GoldMetrics.GoldCheck.Platform.AssetMaintenance.Domain.Model.Events;

public record MachineryDischargedEvent(string MachineryId, string Reason) : IEvent;