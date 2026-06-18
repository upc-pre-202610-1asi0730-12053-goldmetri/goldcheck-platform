using GoldMetrics.GoldCheck.Platform.Analytics.Application.QueryServices;
using GoldMetrics.GoldCheck.Platform.Analytics.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.Analytics.Domain.Model.Queries;
using GoldMetrics.GoldCheck.Platform.Analytics.Domain.Repositories;

namespace GoldMetrics.GoldCheck.Platform.Analytics.Application.Internal.QueryServices;

public class AnalyticsQueryService(IMaterialRepository materialRepository) : IAnalyticsQueryService
{
    public async Task<Material?> Handle(GetRouteProgressByIdQuery query, CancellationToken cancellationToken)
        => await materialRepository.FindByRouteIdAsync(query.RouteId, cancellationToken);
}