using GoldMetrics.GoldCheck.Platform.Analytics.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.Analytics.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.Shared.Application.Model;

namespace GoldMetrics.GoldCheck.Platform.Analytics.Application.CommandServices;

public interface IAnalyticsCommandService
{
    Task<Result<Material>> Handle(ViewRouteProgressCommand command, CancellationToken cancellationToken);
    Task<Result<Material>> Handle(ViewProductionDashboardCommand command, CancellationToken cancellationToken);

}