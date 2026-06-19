using GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Domain.Model.Entities;
using GoldMetrics.GoldCheck.Platform.Shared.Application.Model;

namespace GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Application.CommandServices;

public interface ISubscriptionsBillingCommandService
{
    Task<Result<UserSubscription>> SelectPlanAsync(SelectPlanCommand command, CancellationToken ct = default);
    Task<Result<UserSubscription>> ConfirmSubscriptionAsync(ConfirmSubscriptionCommand command, CancellationToken ct = default);
    Task<Result<UserSubscription>> RequestDowngradeAsync(RequestDowngradeCommand command, CancellationToken ct = default);
    Task<Result<UserSubscription>> ExecuteDowngradeAsync(ExecuteDowngradeCommand command, CancellationToken ct = default);
    Task<Result<UserSubscription>> RestrictFeaturesAsync(RestrictFeaturesCommand command, CancellationToken ct = default);
    Task<Result<UserSubscription>> CheckFeatureAccessAsync(CheckFeatureAccessCommand command, CancellationToken ct = default);
    Task<Result<UserSubscription>> RequestAccessAsync(RequestAccessCommand command, CancellationToken ct = default);
    Task<Result<UserSubscription>> DecideAccessAsync(DecideAccessCommand command, CancellationToken ct = default);
    Task<Result<UserSubscription>> RequestPaymentHistoryAsync(RequestPaymentHistoryCommand command, CancellationToken ct = default);
    Task<Result<UserSubscription>> ViewPaymentHistoryAsync(ViewPaymentHistoryCommand command, CancellationToken ct = default);
    Task<Result<UserSubscription>> RequestInvoiceAsync(RequestInvoiceCommand command, CancellationToken ct = default);
    Task<Result<Invoice>> GenerateInvoiceAsync(GenerateInvoiceCommand command, CancellationToken ct = default);
    Task<Result<Invoice>> DownloadInvoiceAsync(DownloadInvoiceCommand command, CancellationToken ct = default);
    Task<Result<UserSubscription>> CheckUserPlanAsync(CheckUserPlanCommand command, CancellationToken ct = default);
    Task<Result<UserSubscription>> AssignFeaturesAsync(AssignFeaturesCommand command, CancellationToken ct = default);
}