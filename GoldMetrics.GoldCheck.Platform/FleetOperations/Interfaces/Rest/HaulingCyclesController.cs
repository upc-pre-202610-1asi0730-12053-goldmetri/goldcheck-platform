using System.Net.Mime;
using GoldMetrics.GoldCheck.Platform.FleetOperations.Application.CommandServices;
using GoldMetrics.GoldCheck.Platform.FleetOperations.Interfaces.Rest.Resources;
using GoldMetrics.GoldCheck.Platform.FleetOperations.Interfaces.Rest.Transform;
using GoldMetrics.GoldCheck.Platform.Shared.Interfaces.Rest.ProblemDetails;
using GoldMetrics.GoldCheck.Platform.Shared.Resources.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;

namespace GoldMetrics.GoldCheck.Platform.FleetOperations.Interfaces.Rest;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Hauling Cycle Endpoints.")]
public class HaulingCyclesController(
    IHaulingCycleCommandService haulingCycleCommandService,
    IStringLocalizer<ErrorMessages> errorLocalizer,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    [HttpPost]
    [SwaggerOperation("Start Hauling Cycle", "Start a new hauling cycle.", OperationId = "StartHaulingCycle")]
    [SwaggerResponse(201, "The hauling cycle was started.", typeof(HaulingCycleResource))]
    [SwaggerResponse(400, "Invalid request data.")]
    public async Task<IActionResult> StartHaulingCycle([FromBody] StartHaulingCycleResource resource, CancellationToken cancellationToken)
    {
        var command = StartHaulingCycleCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await haulingCycleCommandService.Handle(command, cancellationToken);

        return FleetOperationsActionResultAssembler.ToActionResultFromHaulingCycleResult(
            this, result, errorLocalizer, problemDetailsFactory,
            cycle => Created(string.Empty, HaulingCycleResourceFromEntityAssembler.ToResourceFromEntity(cycle)));
    }
}
