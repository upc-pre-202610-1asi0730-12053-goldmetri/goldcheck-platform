using GoldMetrics.GoldCheck.Platform.AssetMaintenance.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.AssetMaintenance.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.Shared.Application.Model;

namespace GoldMetrics.GoldCheck.Platform.AssetMaintenance.Application.CommandServices;

public interface IAssetMaintenanceCommandService
{
    Task<Result<Machinery>> Handle(RegisterMachineryCommand command, CancellationToken cancellationToken);
    Task<Result<Machinery>> Handle(UpdateMachineryDataCommand command, CancellationToken cancellationToken);
    Task<Result<Machinery>> Handle(SchedulePreventiveMaintenanceCommand command, CancellationToken cancellationToken);
    Task<Result<Machinery>> Handle(DischargeMachineryCommand command, CancellationToken cancellationToken);
}