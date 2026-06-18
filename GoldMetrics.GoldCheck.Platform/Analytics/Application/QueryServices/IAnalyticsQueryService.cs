using GoldMetrics.GoldCheck.Platform.Analytics.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.Analytics.Domain.Model.Queries;

namespace GoldMetrics.GoldCheck.Platform.Analytics.Application.QueryServices;

public interface IAnalyticsQueryService
{
    Task<Material?> Handle(GetRouteProgressByIdQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<Material>> Handle(GetAllRoutesQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<Material>> Handle(GetProductionDataByPeriodQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<Material>> Handle(GetProductionDashboardQuery query, CancellationToken cancellationToken);

}