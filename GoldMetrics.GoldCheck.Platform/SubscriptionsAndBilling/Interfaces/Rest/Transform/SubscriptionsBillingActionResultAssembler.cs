using GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Domain.Model.Entities;
using GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Domain.Model.Errors;
using GoldMetrics.GoldCheck.Platform.Shared.Application.Model;
using GoldMetrics.GoldCheck.Platform.Shared.Interfaces.Rest.ProblemDetails;
using Microsoft.AspNetCore.Mvc;

namespace GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Interfaces.Rest.Transform;

public class SubscriptionsBillingActionResultAssembler(ProblemDetailsFactory factory)
{
    private static int ToStatusCode(SubscriptionsBillingError error) => error switch
    {
        SubscriptionsBillingError.UserSubscriptionNotFound => StatusCodes.Status404NotFound,
        SubscriptionsBillingError.InvoiceNotFound => StatusCodes.Status404NotFound,
        SubscriptionsBillingError.InvalidPlanType => StatusCodes.Status400BadRequest,
        SubscriptionsBillingError.InvalidBillingCycle => StatusCodes.Status400BadRequest,
        SubscriptionsBillingError.PaymentMethodValidationFailed => StatusCodes.Status422UnprocessableEntity,
        SubscriptionsBillingError.FeatureNotAvailableInPlan => StatusCodes.Status403Forbidden,
        SubscriptionsBillingError.DowngradeNotAllowed => StatusCodes.Status409Conflict,
        SubscriptionsBillingError.AccessDenied => StatusCodes.Status403Forbidden,
        SubscriptionsBillingError.OperationCancelled => StatusCodes.Status409Conflict,
        SubscriptionsBillingError.DatabaseError => StatusCodes.Status500InternalServerError,
        SubscriptionsBillingError.InternalServerError => StatusCodes.Status500InternalServerError,
        _ => StatusCodes.Status400BadRequest
    };

    public IActionResult ToActionResult(Result<UserSubscription> result, ControllerBase controller) =>
        result.IsSuccess
            ? controller.Ok(UserSubscriptionResourceFromEntityAssembler.ToResponseFromEntity(result.Value!))
            : factory.CreateProblemDetails(controller, ToStatusCode((SubscriptionsBillingError)result.Error!), result.Error, result.Message);

    public IActionResult ToCreatedActionResult(Result<UserSubscription> result, ControllerBase controller) =>
        result.IsSuccess
            ? controller.CreatedAtAction("GetUserSubscriptionByUserId", new { userId = result.Value!.UserId.Value },
                UserSubscriptionResourceFromEntityAssembler.ToResponseFromEntity(result.Value!))
            : factory.CreateProblemDetails(controller, ToStatusCode((SubscriptionsBillingError)result.Error!), result.Error, result.Message);

    public IActionResult ToCollectionActionResult(Result<IEnumerable<UserSubscription>> result, ControllerBase controller) =>
        result.IsSuccess
            ? controller.Ok(result.Value!.Select(UserSubscriptionResourceFromEntityAssembler.ToResponseFromEntity))
            : factory.CreateProblemDetails(controller, ToStatusCode((SubscriptionsBillingError)result.Error!), result.Error, result.Message);

    public IActionResult ToInvoiceActionResult(Result<Invoice> result, ControllerBase controller) =>
        result.IsSuccess
            ? controller.Ok(UserSubscriptionResourceFromEntityAssembler.ToInvoiceResponseFromEntity(result.Value!))
            : factory.CreateProblemDetails(controller, ToStatusCode((SubscriptionsBillingError)result.Error!), result.Error, result.Message);

    public IActionResult ToInvoiceCollectionActionResult(Result<IEnumerable<Invoice>> result, ControllerBase controller) =>
        result.IsSuccess
            ? controller.Ok(result.Value!.Select(UserSubscriptionResourceFromEntityAssembler.ToInvoiceResponseFromEntity))
            : factory.CreateProblemDetails(controller, ToStatusCode((SubscriptionsBillingError)result.Error!), result.Error, result.Message);

    public IActionResult ToFeaturesActionResult(Result<IEnumerable<string>> result, ControllerBase controller) =>
        result.IsSuccess
            ? controller.Ok(result.Value)
            : factory.CreateProblemDetails(controller, ToStatusCode((SubscriptionsBillingError)result.Error!), result.Error, result.Message);
}