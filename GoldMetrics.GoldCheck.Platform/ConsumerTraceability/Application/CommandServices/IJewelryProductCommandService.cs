using GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.Shared.Application.Model;

namespace GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Application.CommandServices;

public interface IJewelryProductCommandService
{
    Task<Result<JewelryProduct>> Handle(ScanProductQRCommand command,
        CancellationToken cancellationToken = default);
}