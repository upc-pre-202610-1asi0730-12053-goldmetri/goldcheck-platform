namespace GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Interfaces.Acl;

public interface ISubscriptionsBillingContextFacade
{
    Task<bool> ValidateUserSubscriptionExists(string userId, CancellationToken cancellationToken);
    Task<bool> FetchFeatureAccess(string userId, string featureKey, CancellationToken cancellationToken);
}
