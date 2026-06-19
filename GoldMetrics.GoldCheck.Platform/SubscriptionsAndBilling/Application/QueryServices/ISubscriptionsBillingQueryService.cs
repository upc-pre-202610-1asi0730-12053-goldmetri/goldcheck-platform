using GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Domain.Model.Entities;
using GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Domain.Model.Queries;
using GoldMetrics.GoldCheck.Platform.Shared.Application.Model;

namespace GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Application.QueryServices;

public interface ISubscriptionsBillingQueryService
{
    Task<Result<UserSubscription>> GetUserSubscriptionByUserIdAsync(GetUserSubscriptionByUserIdQuery query, CancellationToken ct = default);
    Task<Result<IEnumerable<UserSubscription>>> GetAllUserSubscriptionsAsync(GetAllUserSubscriptionsQuery query, CancellationToken ct = default);
    Task<Result<IEnumerable<Invoice>>> GetPaymentHistoryByUserAsync(GetPaymentHistoryByUserQuery query, CancellationToken ct = default);
    Task<Result<Invoice>> GetInvoiceByIdAsync(GetInvoiceByIdQuery query, CancellationToken ct = default);
    Task<Result<IEnumerable<string>>> GetPlanFeaturesAsync(GetPlanFeaturesQuery query, CancellationToken ct = default);
}