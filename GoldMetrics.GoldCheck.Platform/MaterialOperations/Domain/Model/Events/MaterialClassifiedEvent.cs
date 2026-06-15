using GoldMetrics.GoldCheck.Platform.Shared.Domain.Model.Events;

namespace GoldMetrics.GoldCheck.Platform.MaterialOperations.Domain.Model.Events;

public record MaterialClassifiedEvent(string BatchId, string Classification) : IEvent;
