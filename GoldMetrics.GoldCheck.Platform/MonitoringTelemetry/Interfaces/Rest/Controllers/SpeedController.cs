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
[Route("api/v1/monitoring/speed")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Monitoring & Telemetry — Speed Endpoints.")]
public class SpeedController(
    ISpeedCommandService commandService,
    IStringLocalizer<ErrorMessages> errorLocalizer,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    // POST api/v1/monitoring/speed/monitor
    [HttpPost("monitor")]
    [SwaggerOperation("MonitorSpeedStatus", "Starts speed monitoring for an asset.")]
    [ProducesResponseType(typeof(SpeedReadingResource), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> MonitorSpeed(
        [FromBody] MonitorSpeedStatusResource resource, CancellationToken cancellationToken)
    {
        var command = MonitorSpeedCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await commandService.Handle(command, cancellationToken);
        return MonitoringTelemetryActionResultAssembler.ToActionResult(this, result, errorLocalizer, problemDetailsFactory,
            r => CreatedAtAction(nameof(GetByAsset), new { assetId = r.AssetId.Value },
                SpeedReadingResourceFromEntityAssembler.ToResourceFromEntity(r)));
    }
}