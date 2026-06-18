sing System.Net.Mime;
using GoldMetrics.GoldCheck.Platform.AssetMaintenance.Application.CommandServices;
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
            m => Created(string.Empty, MachineryResourceFromEntityAssembler.ToResourceFromEntity(m)));
    }
}