using GoldMetrics.GoldCheck.Platform.AssetMaintenance.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.AssetMaintenance.Domain.Repositories;
using GoldMetrics.GoldCheck.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using GoldMetrics.GoldCheck.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace GoldMetrics.GoldCheck.Platform.AssetMaintenance.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class MachineryRepository(AppDbContext context) : BaseRepository<Machinery>(context), IMachineryRepository
{
}