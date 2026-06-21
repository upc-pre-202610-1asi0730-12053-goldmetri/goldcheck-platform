using GoldMetrics.GoldCheck.Platform.Analytics.Application.QueryServices;
using GoldMetrics.GoldCheck.Platform.Analytics.Domain.Model.Queries;
using GoldMetrics.GoldCheck.Platform.Analytics.Interfaces.Acl;

namespace GoldMetrics.GoldCheck.Platform.Analytics.Application.Acl;

public class AnalyticsContextFacade(IAnalyticsQueryService analyticsQueryService)
    : IAnalyticsContextFacade
{
    public async Task<bool> ValidateRouteProgressExists(string routeId, CancellationToken cancellationToken)
    {
        var query = new GetRouteProgressByIdQuery(routeId);
        var result = await analyticsQueryService.Handle(query, cancellationToken);
        return result is not null;
    }

    public async Task<string> FetchRouteStatusByRouteId(string routeId, CancellationToken cancellationToken)
    {
        var query = new GetRouteProgressByIdQuery(routeId);
        var result = await analyticsQueryService.Handle(query, cancellationToken);
        return result?.RouteStatus.Value ?? string.Empty;
    }
}
