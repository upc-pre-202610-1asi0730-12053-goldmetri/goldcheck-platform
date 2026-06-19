using GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Application.QueryServices;
using GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Domain.Model.Errors;
using GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Domain.Model.Queries;
using GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Domain.Repositories;
using GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Resources;
using GoldMetrics.GoldCheck.Platform.Shared.Application.Model;
using Microsoft.Extensions.Localization;

namespace GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Application.Internal.QueryServices;

public class SubscriptionsBillingQueryService(
    IUserSubscriptionRepository repository,
    IStringLocalizer<SubscriptionsBillingMessages> localizer) : ISubscriptionsBillingQueryService
{
    public async Task<Result<UserSubscription>> GetUserSubscriptionByUserIdAsync(GetUserSubscriptionByUserIdQuery query, CancellationToken ct = default)
    {
        var subscription = await repository.FindByUserIdAsync(query.UserId, ct);
        return subscription is null
            ? Result<UserSubscription>.Failure(SubscriptionsBillingError.UserSubscriptionNotFound, localizer[nameof(SubscriptionsBillingError.UserSubscriptionNotFound)])
            : Result<UserSubscription>.Success(subscription);
    }
}