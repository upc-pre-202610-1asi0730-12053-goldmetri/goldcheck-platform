using GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Application.CommandServices;
using GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Interfaces.Rest.Resources;
using GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Interfaces.Rest.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Interfaces.Rest;

[ApiController]
[Route("api/v1/subscriptions")]
[Produces("application/json")]
public class SubscriptionsController(
    ISubscriptionsBillingCommandService commandService,
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
}