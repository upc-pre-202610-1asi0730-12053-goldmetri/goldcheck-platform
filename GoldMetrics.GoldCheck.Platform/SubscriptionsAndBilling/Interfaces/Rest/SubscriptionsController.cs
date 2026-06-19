using GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Application.CommandServices;
using GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Interfaces.Rest.Resources;
using GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Interfaces.Rest.Transform;
using GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Domain.Model.Queries;
using GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Application.Internal.QueryServices;
using GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Application.QueryServices;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Interfaces.Rest;


[ApiController]
[Route("api/v1/subscriptions")]
[Produces("application/json")]
public class SubscriptionsController(
    ISubscriptionsBillingCommandService commandService,
    ISubscriptionsBillingQueryService queryService,
    SubscriptionsBillingActionResultAssembler assembler) : ControllerBase
{
    [HttpPost]
    [SwaggerOperation(Summary = "Select a subscription plan", OperationId = "SelectPlan")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SelectPlan([FromBody] SelectPlanResource resource, CancellationToken ct)
    {
        var command = new SelectPlanCommand(resource.UserId, resource.PlanType, resource.BillingCycle);
        var result = await commandService.SelectPlanAsync(command, ct);
        return assembler.ToCreatedActionResult(result, this);
    }
    
    [HttpGet("{userId}", Name = "GetUserSubscriptionByUserId")]
    [SwaggerOperation(Summary = "Get subscription by user ID", OperationId = "GetUserSubscriptionByUserId")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserSubscriptionByUserId([FromRoute] string userId, CancellationToken ct)
    {
        var result = await queryService.GetUserSubscriptionByUserIdAsync(new GetUserSubscriptionByUserIdQuery(userId), ct);
        return assembler.ToActionResult(result, this);
    }
    
    [HttpGet]
    [SwaggerOperation(Summary = "Get all user subscriptions", OperationId = "GetAllUserSubscriptions")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllUserSubscriptions(CancellationToken ct)
    {
        var result = await queryService.GetAllUserSubscriptionsAsync(new GetAllUserSubscriptionsQuery(), ct);
        return assembler.ToCollectionActionResult(result, this);
    }
    
    [HttpPut("{userId}/confirm")]
    [SwaggerOperation(Summary = "Confirm subscription with payment method", OperationId = "ConfirmSubscription")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ConfirmSubscription([FromRoute] string userId, [FromBody] ConfirmSubscriptionResource resource, CancellationToken ct)
    {
        var command = new ConfirmSubscriptionCommand(userId, resource.PaymentMethod);
        var result = await commandService.ConfirmSubscriptionAsync(command, ct);
        return assembler.ToActionResult(result, this);
    }
}