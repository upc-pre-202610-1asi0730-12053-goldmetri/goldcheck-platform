using GoldMetrics.GoldCheck.Platform.JewelryInventory.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.JewelryInventory.Domain.Repositories;
using GoldMetrics.GoldCheck.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using GoldMetrics.GoldCheck.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GoldMetrics.GoldCheck.Platform.JewelryInventory.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class JewelryRepository(AppDbContext context)
    : BaseRepository<Jewelry>(context), IJewelryRepository
{
    public async Task<Jewelry?> FindByCertificateIdAsync(
        string certificateId,
        CancellationToken cancellationToken = default)
        => await Context.Set<Jewelry>()
            .FirstOrDefaultAsync(j => j.CertificateId.Value == certificateId, cancellationToken);
}
