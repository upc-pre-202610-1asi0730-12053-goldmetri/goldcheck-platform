using System.Net.Mime;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Application.CommandServices;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Application.QueryServices;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Queries;
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
[Route("api/v1/monitoring/gnss")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Monitoring & Telemetry — GNSS Endpoints.")]
public class GNSSController(
    IGNSSCommandService commandService,
    IGNSSQueryService queryService,
    IStringLocalizer<ErrorMessages> errorLocalizer,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    // POST api/v1/monitoring/gnss/monitor
    [HttpPost("monitor")]
    [SwaggerOperation("MonitorGNSSStatus", "Starts GNSS status monitoring for an asset.")]
    [ProducesResponseType(typeof(GNSSStatusResource), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> MonitorGNSS(
        [FromBody] MonitorGNSSStatusResource resource, CancellationToken cancellationToken)
    {
        var command = MonitorGNSSCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await commandService.Handle(command, cancellationToken);
        return MonitoringTelemetryActionResultAssembler.ToActionResult(this, result, errorLocalizer, problemDetailsFactory,
            s => CreatedAtAction(nameof(GetByAsset), new { assetId = s.AssetId.Value },
                GNSSStatusResourceFromEntityAssembler.ToResourceFromEntity(s)));
    }
    
    // POST api/v1/monitoring/gnss/{assetId}/anomalies/detect
    [HttpPost("{assetId}/anomalies/detect")]
    [SwaggerOperation("DetectGNSSAnomaly", "Detects a GNSS anomaly (chip off).")]
    [ProducesResponseType(typeof(GNSSStatusResource), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DetectAnomaly(string assetId, CancellationToken cancellationToken)
    {
        var result = await commandService.Handle(new DetectGNSSAnomalyCommand(assetId), cancellationToken);
        return MonitoringTelemetryActionResultAssembler.ToActionResult(this, result, errorLocalizer, problemDetailsFactory,
            s => Ok(GNSSStatusResourceFromEntityAssembler.ToResourceFromEntity(s)));
    }
    
    
    // POST api/v1/monitoring/gnss/{assetId}/restart
    [HttpPost("{assetId}/restart")]
    [SwaggerOperation("RestartGNSS", "Restarts GNSS and logs the event.")]
    [ProducesResponseType(typeof(GNSSStatusResource), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RestartGNSS(
        string assetId, [FromBody] RestartGNSSResource resource, CancellationToken cancellationToken)
    {
        var command = RestartGNSSCommandFromResourceAssembler.ToCommandFromResource(assetId, resource);
        var result = await commandService.Handle(command, cancellationToken);
        return MonitoringTelemetryActionResultAssembler.ToActionResult(this, result, errorLocalizer, problemDetailsFactory,
            s => Ok(GNSSStatusResourceFromEntityAssembler.ToResourceFromEntity(s)));
    }
    
    // GET api/v1/monitoring/gnss/{assetId}
    [HttpGet("{assetId}")]
    [SwaggerOperation("GetGNSSStatusesByAsset", "Returns GNSS status records for an asset.")]
    [ProducesResponseType(typeof(IEnumerable<GNSSStatusResource>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByAsset(string assetId, CancellationToken cancellationToken)
    {
        var statuses = await queryService.Handle(new GetGNSSStatusByAssetQuery(assetId), cancellationToken);
        return Ok(statuses.Select(GNSSStatusResourceFromEntityAssembler.ToResourceFromEntity));
    }
}