using GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.Shared.Domain.Repositories;

namespace GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Domain.Repositories;

public interface IJewelryProductRepository : IBaseRepository<JewelryProduct>
{
    Task<JewelryProduct?> FindByQRCodeAsync(string qrCode,
        CancellationToken cancellationToken = default);
    
    Task<JewelryProduct?> FindByCertificateIdAsync(string certificateId,
        CancellationToken cancellationToken = default);
}