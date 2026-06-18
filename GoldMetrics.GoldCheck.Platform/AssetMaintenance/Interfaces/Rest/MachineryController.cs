using System.Net.Mime;
using GoldMetrics.GoldCheck.Platform.AssetMaintenance.Application.CommandServices;
using GoldMetrics.GoldCheck.Platform.AssetMaintenance.Application.QueryServices;
using GoldMetrics.GoldCheck.Platform.AssetMaintenance.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.AssetMaintenance.Domain.Model.Queries;
using GoldMetrics.GoldCheck.Platform.AssetMaintenance.Interfaces.Rest.Resources;
using GoldMetrics.GoldCheck.Platform.AssetMaintenance.Interfaces.Rest.Transform;
using GoldMetrics.GoldCheck.Platform.Shared.Interfaces.Rest.ProblemDetails;
using GoldMetrics.GoldCheck.Platform.Shared.Resources.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;

namespace GoldMetrics.GoldCheck.Platform.AssetMaintenance.Interfaces.Rest;

[ApiController]
[Route("api/v1/machinery")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Asset and Maintenance Management Endpoints.")]
public class MachineryController(
    IAssetMaintenanceCommandService commandService,
    IStringLocalizer<ErrorMessages> errorLocalizer,
    IAssetMaintenanceQueryService queryService,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase

{
    [HttpPost]
    [SwaggerOperation("Register Machinery", "Register a new machinery asset.", OperationId = "RegisterMachinery")]
    [SwaggerResponse(201, "Machinery registered.", typeof(MachineryResource))]
    [SwaggerResponse(400, "Invalid request data.")]
    public async Task<IActionResult> RegisterMachinery([FromBody] RegisterMachineryResource resource, CancellationToken cancellationToken)
    {
        var command = new RegisterMachineryCommand(resource.MachineryId, resource.Model, resource.OEM);
        var result = await commandService.Handle(command, cancellationToken);
        return AssetMaintenanceActionResultAssembler.ToActionResultFromMachineryResult(
            this, result, errorLocalizer, problemDetailsFactory,
            m => CreatedAtAction(nameof(GetMachineryById), new { machineryId = m.MachineryId.Value },
                MachineryResourceFromEntityAssembler.ToResourceFromEntity(m)));
    }
    
    [HttpGet("{machineryId}")]
    [SwaggerOperation("Get Machinery By Id", "Get a machinery asset by its identifier.", OperationId = "GetMachineryById")]
    [SwaggerResponse(200, "Machinery found.", typeof(MachineryResource))]
    [SwaggerResponse(404, "Machinery not found.")]
    public async Task<IActionResult> GetMachineryById(string machineryId, CancellationToken cancellationToken)
    {
        var machinery = await queryService.Handle(new GetMachineryByIdQuery(machineryId), cancellationToken);
        return AssetMaintenanceActionResultAssembler.ToActionResultFromGetMachineryResult(
            this, machinery, errorLocalizer, problemDetailsFactory,
            m => Ok(MachineryResourceFromEntityAssembler.ToResourceFromEntity(m)));
    }
    [HttpGet]
    [SwaggerOperation("Get All Machinery", "Get all machinery assets.", OperationId = "GetAllMachinery")]
    [SwaggerResponse(200, "Machinery list found.", typeof(IEnumerable<MachineryResource>))]
    public async Task<IActionResult> GetAllMachinery(CancellationToken cancellationToken)
    {
        var machinery = await queryService.Handle(new GetAllMachineryQuery(), cancellationToken);
        return Ok(machinery.Select(MachineryResourceFromEntityAssembler.ToResourceFromEntity));
    }
}