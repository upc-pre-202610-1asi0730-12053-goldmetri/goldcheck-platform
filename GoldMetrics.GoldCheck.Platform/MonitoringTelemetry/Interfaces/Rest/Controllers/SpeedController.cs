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
    ISpeedQueryService queryService,
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
    
    // POST api/v1/monitoring/speed/{assetId}/detect-excess
    [HttpPost("{assetId}/detect-excess")]
    [SwaggerOperation("DetectSpeedExcess", "Detects and logs speed excess for an asset.")]
    [ProducesResponseType(typeof(SpeedReadingResource), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DetectExcess(
        string assetId, [FromBody] DetectSpeedExcessResource resource, CancellationToken cancellationToken)
    {
        var command = DetectSpeedExcessCommandFromResourceAssembler.ToCommandFromResource(assetId, resource);
        var result = await commandService.Handle(command, cancellationToken);
        return MonitoringTelemetryActionResultAssembler.ToActionResult(this, result, errorLocalizer, problemDetailsFactory,
            r => Ok(SpeedReadingResourceFromEntityAssembler.ToResourceFromEntity(r)));
    }
    
    // GET api/v1/monitoring/speed/{assetId}
    [HttpGet("{assetId}")]
    [SwaggerOperation("GetSpeedReadingsByAsset", "Returns speed readings for an asset.")]
    [ProducesResponseType(typeof(IEnumerable<SpeedReadingResource>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByAsset(string assetId, CancellationToken cancellationToken)
    {
        var readings = await queryService.Handle(new GetSpeedReadingByAssetQuery(assetId), cancellationToken);
        return Ok(readings.Select(SpeedReadingResourceFromEntityAssembler.ToResourceFromEntity));
    }
}