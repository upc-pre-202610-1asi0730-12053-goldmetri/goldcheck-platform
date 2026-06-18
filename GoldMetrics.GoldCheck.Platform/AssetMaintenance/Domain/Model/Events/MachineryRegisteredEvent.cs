using GoldMetrics.GoldCheck.Platform.Shared.Domain.Model.Events;

namespace GoldMetrics.GoldCheck.Platform.AssetMaintenance.Domain.Model.Events;

public record MachineryRegisteredEvent(string MachineryId, string Model, string OEM) : IEvent;