using GoldMetrics.GoldCheck.Platform.Analytics.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.Analytics.Domain.Repositories;
using GoldMetrics.GoldCheck.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using GoldMetrics.GoldCheck.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace GoldMetrics.GoldCheck.Platform.Analytics.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class MaterialRepository(AppDbContext context) : BaseRepository<Material>(context), IMaterialRepository
{
}