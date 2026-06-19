using GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.Shared.Domain.Repositories;

namespace GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Domain.Repositories;

public interface IUserSubscriptionRepository : IBaseRepository<UserSubscription>
{
    Task<UserSubscription?> FindByUserIdAsync(string userId, CancellationToken ct = default);
}