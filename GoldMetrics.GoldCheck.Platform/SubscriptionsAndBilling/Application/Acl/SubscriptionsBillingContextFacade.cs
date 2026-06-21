using GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Application.QueryServices;
using GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Domain.Model.Queries;
using GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Interfaces.Acl;

namespace GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Application.Acl;

public class SubscriptionsBillingContextFacade(ISubscriptionsBillingQueryService subscriptionsBillingQueryService)
    : ISubscriptionsBillingContextFacade
{
    public async Task<bool> ValidateUserSubscriptionExists(string userId, CancellationToken cancellationToken)
    {
        var query = new GetUserSubscriptionByUserIdQuery(userId);
        var result = await subscriptionsBillingQueryService.GetUserSubscriptionByUserIdAsync(query, cancellationToken);
        return result.IsSuccess && result.Value is not null;
    }

    public async Task<bool> FetchFeatureAccess(string userId, string featureKey, CancellationToken cancellationToken)
    {
        var query = new GetUserSubscriptionByUserIdQuery(userId);
        var result = await subscriptionsBillingQueryService.GetUserSubscriptionByUserIdAsync(query, cancellationToken);
        if (!result.IsSuccess || result.Value is null) return false;
        return result.Value.AssignedFeatures.Contains(featureKey);
    }
}
