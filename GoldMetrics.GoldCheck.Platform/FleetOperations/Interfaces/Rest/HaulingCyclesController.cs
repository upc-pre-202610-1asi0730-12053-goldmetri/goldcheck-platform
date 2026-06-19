using System.Net.Mime;
using GoldMetrics.GoldCheck.Platform.FleetOperations.Application.CommandServices;
using GoldMetrics.GoldCheck.Platform.FleetOperations.Application.QueryServices;
using GoldMetrics.GoldCheck.Platform.FleetOperations.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.FleetOperations.Domain.Model.Queries;
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
    IHaulingCycleQueryService haulingCycleQueryService,
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
            cycle => CreatedAtAction(nameof(GetHaulingCycleById),
                new { cycleId = cycle.Id },
                HaulingCycleResourceFromEntityAssembler.ToResourceFromEntity(cycle)));
    }

    [HttpPut("{cycleId:int}/load")]
    [SwaggerOperation("Load Material", "Load material into a hauling cycle.", OperationId = "LoadMaterial")]
    [SwaggerResponse(200, "Material loaded successfully.", typeof(HaulingCycleResource))]
    [SwaggerResponse(400, "Invalid payload.")]
    [SwaggerResponse(404, "The hauling cycle was not found.")]
    public async Task<IActionResult> LoadMaterial(int cycleId, [FromBody] LoadMaterialResource resource, CancellationToken cancellationToken)
    {
        var command = new LoadMaterialCommand(cycleId, resource.PayloadTons);
        var result = await haulingCycleCommandService.Handle(command, cancellationToken);

        return FleetOperationsActionResultAssembler.ToActionResultFromHaulingCycleResult(
            this, result, errorLocalizer, problemDetailsFactory,
            cycle => Ok(HaulingCycleResourceFromEntityAssembler.ToResourceFromEntity(cycle)));
    }

    [HttpGet]
    [SwaggerOperation("Get All Hauling Cycles", "Get all hauling cycles.", OperationId = "GetAllHaulingCycles")]
    [SwaggerResponse(200, "The hauling cycles were found.", typeof(IEnumerable<HaulingCycleResource>))]
    public async Task<IActionResult> GetAllHaulingCycles(CancellationToken cancellationToken)
    {
        var query = new GetAllHaulingCyclesQuery();
        var cycles = await haulingCycleQueryService.Handle(query, cancellationToken);
        return Ok(cycles.Select(HaulingCycleResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{cycleId:int}")]
    [SwaggerOperation("Get Hauling Cycle by Id", "Get a hauling cycle by its identifier.", OperationId = "GetHaulingCycleById")]
    [SwaggerResponse(200, "The hauling cycle was found.", typeof(HaulingCycleResource))]
    [SwaggerResponse(404, "The hauling cycle was not found.")]
    public async Task<IActionResult> GetHaulingCycleById(int cycleId, CancellationToken cancellationToken)
    {
        var query = new GetHaulingCycleByIdQuery(cycleId);
        var cycle = await haulingCycleQueryService.Handle(query, cancellationToken);

        return FleetOperationsActionResultAssembler.ToActionResultFromGetHaulingCycleResult(
            this, cycle, errorLocalizer, problemDetailsFactory,
            c => Ok(HaulingCycleResourceFromEntityAssembler.ToResourceFromEntity(c)));
    }
}
