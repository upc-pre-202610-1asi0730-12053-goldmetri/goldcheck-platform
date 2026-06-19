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
    
    public async Task<Result<UserSubscription>> RestrictFeaturesAsync(RestrictFeaturesCommand command, CancellationToken ct = default)
    {
        try
        {
            var subscription = await repository.FindByUserIdAsync(command.UserId, ct);
            if (subscription is null) return NotFound<UserSubscription>();
            subscription.RestrictFeatures(command);
            repository.Update(subscription);
            await unitOfWork.CompleteAsync(ct);
            return Result<UserSubscription>.Success(subscription);
        }
        catch (OperationCanceledException) { return Cancelled<UserSubscription>(); }
        catch (Exception) { return ServerError<UserSubscription>(); }
    }
    
    public async Task<Result<UserSubscription>> RequestDowngradeAsync(RequestDowngradeCommand command, CancellationToken ct = default)
    {
        try
        {
            var subscription = await repository.FindByUserIdAsync(command.UserId, ct);
            if (subscription is null) return NotFound<UserSubscription>();
            subscription.RequestDowngrade(command);
            repository.Update(subscription);
            await unitOfWork.CompleteAsync(ct);
            return Result<UserSubscription>.Success(subscription);
        }
        catch (OperationCanceledException) { return Cancelled<UserSubscription>(); }
        catch (Exception) { return ServerError<UserSubscription>(); }
    }
    
    public async Task<Result<UserSubscription>> ExecuteDowngradeAsync(ExecuteDowngradeCommand command, CancellationToken ct = default)
    {
        try
        {
            var subscription = await repository.FindByUserIdAsync(command.UserId, ct);
            if (subscription is null) return NotFound<UserSubscription>();
            if (subscription.SubscriptionStatus.Value != "DowngradeRequested")
                return Result<UserSubscription>.Failure(SubscriptionsBillingError.DowngradeNotAllowed, localizer[nameof(SubscriptionsBillingError.DowngradeNotAllowed)]);
            subscription.ExecuteDowngrade(command);
            repository.Update(subscription);
            await unitOfWork.CompleteAsync(ct);
            return Result<UserSubscription>.Success(subscription);
        }
        catch (OperationCanceledException) { return Cancelled<UserSubscription>(); }
        catch (Exception) { return ServerError<UserSubscription>(); }
    }
    
    public async Task<Result<UserSubscription>> DecideAccessAsync(DecideAccessCommand command, CancellationToken ct = default)
    {
        try
        {
            var subscription = await repository.FindByUserIdAsync(command.UserId, ct);
            if (subscription is null) return NotFound<UserSubscription>();
            subscription.DecideAccess(command);
            repository.Update(subscription);
            await unitOfWork.CompleteAsync(ct);
            return Result<UserSubscription>.Success(subscription);
        }
        catch (OperationCanceledException) { return Cancelled<UserSubscription>(); }
        catch (Exception) { return ServerError<UserSubscription>(); }
    }
    
    public async Task<Result<UserSubscription>> CheckFeatureAccessAsync(CheckFeatureAccessCommand command, CancellationToken ct = default)
    {
        try
        {
            var subscription = await repository.FindByUserIdAsync(command.UserId, ct);
            if (subscription is null) return NotFound<UserSubscription>();
            subscription.CheckFeatureAccess(command);
            repository.Update(subscription);
            await unitOfWork.CompleteAsync(ct);
            return Result<UserSubscription>.Success(subscription);
        }
        catch (OperationCanceledException) { return Cancelled<UserSubscription>(); }
        catch (Exception) { return ServerError<UserSubscription>(); }
    }
    
    public async Task<Result<UserSubscription>> ViewPaymentHistoryAsync(ViewPaymentHistoryCommand command, CancellationToken ct = default)
    {
        try
        {
            var subscription = await repository.FindByUserIdAsync(command.UserId, ct);
            if (subscription is null) return NotFound<UserSubscription>();
            subscription.ViewPaymentHistory(command);
            repository.Update(subscription);
            await unitOfWork.CompleteAsync(ct);
            return Result<UserSubscription>.Success(subscription);
        }
        catch (OperationCanceledException) { return Cancelled<UserSubscription>(); }
        catch (Exception) { return ServerError<UserSubscription>(); }
    }
    
    public async Task<Result<UserSubscription>> RequestPaymentHistoryAsync(RequestPaymentHistoryCommand command, CancellationToken ct = default)
    {
        try
        {
            var subscription = await repository.FindByUserIdAsync(command.UserId, ct);
            if (subscription is null) return NotFound<UserSubscription>();
            subscription.RequestPaymentHistory(command);
            repository.Update(subscription);
            await unitOfWork.CompleteAsync(ct);
            return Result<UserSubscription>.Success(subscription);
        }
        catch (OperationCanceledException) { return Cancelled<UserSubscription>(); }
        catch (Exception) { return ServerError<UserSubscription>(); }
    }
    
    public async Task<Result<Invoice>> GenerateInvoiceAsync(GenerateInvoiceCommand command, CancellationToken ct = default)
    {
        try
        {
            var subscription = await repository.FindByUserIdAsync(command.UserId, ct);
            if (subscription is null) return NotFound<Invoice>();
            var invoice = subscription.GenerateInvoice(command);
            repository.Update(subscription);
            await unitOfWork.CompleteAsync(ct);
            return Result<Invoice>.Success(invoice);
        }
        catch (OperationCanceledException) { return Cancelled<Invoice>(); }
        catch (Exception) { return ServerError<Invoice>(); }
    }
    
    public async Task<Result<UserSubscription>> RequestInvoiceAsync(RequestInvoiceCommand command, CancellationToken ct = default)
    {
        try
        {
            var subscription = await repository.FindByUserIdAsync(command.UserId, ct);
            if (subscription is null) return NotFound<UserSubscription>();
            subscription.RequestInvoice(command);
            repository.Update(subscription);
            await unitOfWork.CompleteAsync(ct);
            return Result<UserSubscription>.Success(subscription);
        }
        catch (OperationCanceledException) { return Cancelled<UserSubscription>(); }
        catch (Exception) { return ServerError<UserSubscription>(); }
    }
    
    public async Task<Result<Invoice>> DownloadInvoiceAsync(DownloadInvoiceCommand command, CancellationToken ct = default)
    {
        try
        {
            var subscription = await repository.FindByUserIdAsync(command.UserId, ct);
            if (subscription is null) return NotFound<Invoice>();
            var invoice = subscription.DownloadInvoice(command);
            if (invoice is null)
                return Result<Invoice>.Failure(SubscriptionsBillingError.InvoiceNotFound, localizer[nameof(SubscriptionsBillingError.InvoiceNotFound)]);
            repository.Update(subscription);
            await unitOfWork.CompleteAsync(ct);
            return Result<Invoice>.Success(invoice);
        }
        catch (OperationCanceledException) { return Cancelled<Invoice>(); }
        catch (Exception) { return ServerError<Invoice>(); }
    }
    

    private Result<T> NotFound<T>() =>
        Result<T>.Failure(SubscriptionsBillingError.UserSubscriptionNotFound, localizer[nameof(SubscriptionsBillingError.UserSubscriptionNotFound)]);
    private Result<T> Cancelled<T>() =>
        Result<T>.Failure(SubscriptionsBillingError.OperationCancelled, localizer[nameof(SubscriptionsBillingError.OperationCancelled)]);
    private Result<T> ServerError<T>() =>
        Result<T>.Failure(SubscriptionsBillingError.InternalServerError, localizer[nameof(SubscriptionsBillingError.InternalServerError)]);
}