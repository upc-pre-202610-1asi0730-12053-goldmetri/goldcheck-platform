using GoldMetrics.GoldCheck.Platform.Shared.Domain.Model.Events;

namespace GoldMetrics.GoldCheck.Platform.MaterialOperations.Domain.Model.Events;

public record MineralTypeIdentifiedEvent(string BatchId, string MineralType, decimal PayloadTons) : IEvent;
