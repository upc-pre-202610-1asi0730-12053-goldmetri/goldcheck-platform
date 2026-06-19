using GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Application.CommandServices;
using GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Domain.Model.Entities;
using GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Domain.Model.Errors;
using GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Domain.Repositories;
using GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Resources;
using GoldMetrics.GoldCheck.Platform.Shared.Application.Model;
using GoldMetrics.GoldCheck.Platform.Shared.Domain.Repositories;
using Microsoft.Extensions.Localization;

namespace GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Application.Internal.CommandServices;

public class SubscriptionsBillingCommandService(
    IUserSubscriptionRepository repository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<SubscriptionsBillingMessages> localizer) : ISubscriptionsBillingCommandService
{
    public async Task<Result<UserSubscription>> SelectPlanAsync(SelectPlanCommand command, CancellationToken ct = default)
    {
        try
        {
            var subscription = new UserSubscription(command);
            await repository.AddAsync(subscription, ct);
            await unitOfWork.CompleteAsync(ct);
            return Result<UserSubscription>.Success(subscription);
        }
        catch (OperationCanceledException) { return Cancelled<UserSubscription>(); }
        catch (Exception) { return ServerError<UserSubscription>(); }
    }
    
    public async Task<Result<UserSubscription>> ConfirmSubscriptionAsync(ConfirmSubscriptionCommand command, CancellationToken ct = default)
    {
        try
        {
            var subscription = await repository.FindByUserIdAsync(command.UserId, ct);
            if (subscription is null) return NotFound<UserSubscription>();
            subscription.ConfirmSubscription(command);
            repository.Update(subscription);
            await unitOfWork.CompleteAsync(ct);
            return Result<UserSubscription>.Success(subscription);
        }
        catch (OperationCanceledException) { return Cancelled<UserSubscription>(); }
        catch (Exception) { return ServerError<UserSubscription>(); }
    }

    private Result<T> NotFound<T>() =>
        Result<T>.Failure(SubscriptionsBillingError.UserSubscriptionNotFound, localizer[nameof(SubscriptionsBillingError.UserSubscriptionNotFound)]);
    private Result<T> Cancelled<T>() =>
        Result<T>.Failure(SubscriptionsBillingError.OperationCancelled, localizer[nameof(SubscriptionsBillingError.OperationCancelled)]);
    private Result<T> ServerError<T>() =>
        Result<T>.Failure(SubscriptionsBillingError.InternalServerError, localizer[nameof(SubscriptionsBillingError.InternalServerError)]);
}