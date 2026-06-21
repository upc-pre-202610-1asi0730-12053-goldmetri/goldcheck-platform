namespace GoldMetrics.GoldCheck.Platform.Analytics.Interfaces.Acl;

public interface IAnalyticsContextFacade
{
    Task<bool> ValidateRouteProgressExists(string routeId, CancellationToken cancellationToken);
    Task<string> FetchRouteStatusByRouteId(string routeId, CancellationToken cancellationToken);
}
