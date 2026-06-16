using GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Domain.Model.Queries;

namespace GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Application.QueryServices;

public interface IJewelryProductQueryService
{
    Task<JewelryProduct?> Handle(GetProductByQRQuery query,
        CancellationToken cancellationToken = default);
    
    Task<JewelryProduct?> Handle(GetCertificateByIdQuery query,
        CancellationToken cancellationToken = default);
}