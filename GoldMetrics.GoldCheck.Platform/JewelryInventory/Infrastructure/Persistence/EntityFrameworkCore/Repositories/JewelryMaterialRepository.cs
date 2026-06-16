using GoldMetrics.GoldCheck.Platform.JewelryInventory.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.JewelryInventory.Domain.Repositories;
using GoldMetrics.GoldCheck.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using GoldMetrics.GoldCheck.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GoldMetrics.GoldCheck.Platform.JewelryInventory.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class JewelryMaterialRepository(AppDbContext context)
    : BaseRepository<JewelryMaterial>(context), IJewelryMaterialRepository
{
    public async Task<JewelryMaterial?> FindByMaterialIdAsync(
        string materialId,
        CancellationToken cancellationToken = default)
        => await Context.Set<JewelryMaterial>()
            .FirstOrDefaultAsync(m => m.MaterialId.Value == materialId, cancellationToken);

    public async Task<IEnumerable<JewelryMaterial>> FindAllAsync(
        CancellationToken cancellationToken = default)
        => await Context.Set<JewelryMaterial>().ToListAsync(cancellationToken);
}
