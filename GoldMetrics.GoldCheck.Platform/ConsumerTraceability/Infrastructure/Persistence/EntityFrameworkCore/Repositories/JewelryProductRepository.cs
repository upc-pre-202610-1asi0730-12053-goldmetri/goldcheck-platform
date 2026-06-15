using GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Domain.Repositories;
using GoldMetrics.GoldCheck.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using GoldMetrics.GoldCheck.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class JewelryProductRepository(AppDbContext context)
    : BaseRepository<JewelryProduct>(context), IJewelryProductRepository
{
    public async Task<JewelryProduct?> FindByQRCodeAsync(
        string qrCode,
        CancellationToken cancellationToken = default)
        => await Context.Set<JewelryProduct>()
            .FirstOrDefaultAsync(p => p.QRCode.Value == qrCode, cancellationToken);
}