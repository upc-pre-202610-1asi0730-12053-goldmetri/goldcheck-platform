using GoldMetrics.GoldCheck.Platform.Shared.Domain.Model.Events;

namespace GoldMetrics.GoldCheck.Platform.Analytics.Domain.Model.Events;

public record RouteDataLoadedEvent(string RouteId, string UserId) : IEvent;