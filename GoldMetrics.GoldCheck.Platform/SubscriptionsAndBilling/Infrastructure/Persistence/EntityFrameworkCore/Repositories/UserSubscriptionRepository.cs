using GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Domain.Repositories;
using GoldMetrics.GoldCheck.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using GoldMetrics.GoldCheck.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class UserSubscriptionRepository(AppDbContext context)
    : BaseRepository<UserSubscription>(context), IUserSubscriptionRepository
{
    public async Task<UserSubscription?> FindByUserIdAsync(string userId, CancellationToken ct = default) =>
        await Context.Set<UserSubscription>()
            .Include(s => s.Invoices)
            .FirstOrDefaultAsync(s => s.UserId.Value == userId, ct);
}