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
    [Route("api/v1/monitoring/pressure")]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerTag("Available Monitoring & Telemetry — Pressure Endpoints.")]
    public class PressureController(
        IPressureCommandService commandService,
        IStringLocalizer<ErrorMessages> errorLocalizer,
        IPressureQueryService queryService,
        ProblemDetailsFactory problemDetailsFactory)
        : ControllerBase
    {
        // POST api/v1/monitoring/pressure/monitor
        [HttpPost("monitor")]
        [SwaggerOperation("MonitorPressure", "Starts pressure monitoring for an asset.")]
        [ProducesResponseType(typeof(PressureReadingResource), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> MonitorPressure(
            [FromBody] MonitorPressureResource resource, CancellationToken cancellationToken)
        {
            var command = MonitorPressureCommandFromResourceAssembler.ToCommandFromResource(resource);
            var result = await commandService.Handle(command, cancellationToken);
            return MonitoringTelemetryActionResultAssembler.ToActionResult(this, result, errorLocalizer, problemDetailsFactory,
                r => CreatedAtAction(nameof(GetByAsset), new { assetId = r.AssetId.Value },
                    PressureReadingResourceFromEntityAssembler.ToResourceFromEntity(r)));
        }
        
        // POST api/v1/monitoring/pressure/{assetId}/analyse
        [HttpPost("{assetId}/analyse")]
        [SwaggerOperation("AnalysePressure", "Analyses engine pressure readings.")]
        [ProducesResponseType(typeof(PressureReadingResource), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Analyse(
            string assetId, [FromBody] AnalysePressureResource resource, CancellationToken cancellationToken)
        {
            var command = AnalysePressureCommandFromResourceAssembler.ToCommandFromResource(assetId, resource);
            var result = await commandService.Handle(command, cancellationToken);
            return MonitoringTelemetryActionResultAssembler.ToActionResult(this, result, errorLocalizer, problemDetailsFactory,
                r => Ok(PressureReadingResourceFromEntityAssembler.ToResourceFromEntity(r)));
        }
        
        // POST api/v1/monitoring/pressure/{assetId}/anomalies/detect
        [HttpPost("{assetId}/anomalies/detect")]
        [SwaggerOperation("DetectPressureAnomaly", "Detects and logs a pressure anomaly.")]
        [ProducesResponseType(typeof(PressureReadingResource), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DetectAnomaly(
            string assetId, [FromBody] DetectPressureAnomalyResource resource, CancellationToken cancellationToken)
        {
            var command = DetectPressureAnomalyCommandFromResourceAssembler.ToCommandFromResource(assetId, resource);
            var result = await commandService.Handle(command, cancellationToken);
            return MonitoringTelemetryActionResultAssembler.ToActionResult(this, result, errorLocalizer, problemDetailsFactory,
                r => Ok(PressureReadingResourceFromEntityAssembler.ToResourceFromEntity(r)));
        }
        
        // GET api/v1/monitoring/pressure/{assetId}
        [HttpGet("{assetId}")]
        [SwaggerOperation("GetPressureReadingsByAsset", "Returns pressure readings for an asset.")]
        [ProducesResponseType(typeof(IEnumerable<PressureReadingResource>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByAsset(string assetId, CancellationToken cancellationToken)
        {
            var readings = await queryService.Handle(new GetPressureReadingByAssetQuery(assetId), cancellationToken);
            return Ok(readings.Select(PressureReadingResourceFromEntityAssembler.ToResourceFromEntity));
        }
        
        // GET api/v1/monitoring/pressure/{assetId}/anomalies
        [HttpGet("{assetId}/anomalies")]
        [SwaggerOperation("GetPressureAnomaliesByAsset", "Returns pressure anomalies for an asset.")]
        [ProducesResponseType(typeof(IEnumerable<PressureReadingResource>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAnomalies(string assetId, CancellationToken cancellationToken)
        {
            var readings = await queryService.Handle(new GetPressureAnomaliesByAssetQuery(assetId), cancellationToken);
            return Ok(readings.Select(PressureReadingResourceFromEntityAssembler.ToResourceFromEntity));
        }
    }