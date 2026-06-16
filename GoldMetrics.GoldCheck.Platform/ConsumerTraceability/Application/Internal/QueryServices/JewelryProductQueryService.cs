using GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Application.QueryServices;
using GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Domain.Model.Queries;
using GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Domain.Repositories;

namespace GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Application.Internal.QueryServices;

public class JewelryProductQueryService(IJewelryProductRepository productRepository)
    : IJewelryProductQueryService
{
    public async Task<JewelryProduct?> Handle(
        GetProductByQRQuery query,
        CancellationToken cancellationToken = default)
        => await productRepository.FindByQRCodeAsync(query.QRCode, cancellationToken);
    
    public async Task<JewelryProduct?> Handle(
        GetCertificateByIdQuery query,
        CancellationToken cancellationToken = default)
        => await productRepository.FindByCertificateIdAsync(query.CertificateId, cancellationToken);
}