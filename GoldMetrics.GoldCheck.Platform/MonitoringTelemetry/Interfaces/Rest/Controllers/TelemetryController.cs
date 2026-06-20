using System.Net.Mime;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Application.CommandServices;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Interfaces.Rest.Resources;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Interfaces.Rest.Transform;
using GoldMetrics.GoldCheck.Platform.Shared.Resources.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;
using ProblemDetailsFactory =
    GoldMetrics.GoldCheck.Platform.Shared.Interfaces.Rest.ProblemDetails.ProblemDetailsFactory;

namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Interfaces.Rest.Controllers;

[ApiController]
[Route("api/v1/monitoring/telemetry")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Monitoring & Telemetry — Telemetry Data Endpoints.")]
public class TelemetryController(
    ITelemetryCommandService commandService,
    IStringLocalizer<ErrorMessages> errorLocalizer,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    // POST api/v1/monitoring/telemetry/process
    [HttpPost("process")]
    [SwaggerOperation("ProcessTelemetryData", "Processes raw telemetry data from an asset.")]
    [ProducesResponseType(typeof(TelemetryDataResource), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ProcessTelemetry(
        [FromBody] ProcessTelemetryDataResource resource, CancellationToken cancellationToken)
    {
        var command = ProcessTelemetryCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await commandService.Handle(command, cancellationToken);
        return MonitoringTelemetryActionResultAssembler.ToActionResult(this, result, errorLocalizer, problemDetailsFactory,
            d => CreatedAtAction(nameof(GetByAsset), new { assetId = d.AssetId.Value },
                TelemetryDataResourceFromEntityAssembler.ToResourceFromEntity(d)));
    }
}