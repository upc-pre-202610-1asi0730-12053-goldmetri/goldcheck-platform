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
    [Route("api/v1/monitoring/temperature")]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerTag("Available Monitoring & Telemetry — Temperature Endpoints.")]
    public class TemperatureController(
        ITemperatureCommandService commandService,
        IStringLocalizer<ErrorMessages> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory)
        : ControllerBase
    {
        // POST api/v1/monitoring/temperature/monitor
        [HttpPost("monitor")]
        [SwaggerOperation("MonitorEngineTemperature", "Starts engine temperature monitoring for an asset.")]
        [ProducesResponseType(typeof(TemperatureReadingResource), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> MonitorEngineTemperature(
            [FromBody] MonitorEngineTemperatureResource resource, CancellationToken cancellationToken)
        {
            var command = MonitorEngineTemperatureCommandFromResourceAssembler.ToCommandFromResource(resource);
            var result = await commandService.Handle(command, cancellationToken);
            return MonitoringTelemetryActionResultAssembler.ToActionResult(this, result, errorLocalizer, problemDetailsFactory,
                r => CreatedAtAction(nameof(GetByAsset), new { assetId = r.AssetId.Value },
                    TemperatureReadingResourceFromEntityAssembler.ToResourceFromEntity(r)));
        }
        
        // POST api/v1/monitoring/temperature/{assetId}/analyse/exhaust
        [HttpPost("{assetId}/analyse/exhaust")]
        [SwaggerOperation("AnalyseExhaustTemperature", "Analyses exhaust temperature for an asset.")]
        [ProducesResponseType(typeof(TemperatureReadingResource), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AnalyseExhaust(
            string assetId, [FromBody] AnalyseExhaustTemperatureResource resource, CancellationToken cancellationToken)
        {
            var command = TemperatureReadingResourceFromEntityAssembler.AnalyseExhaustCommandFromResourceAssembler.ToCommandFromResource(assetId, resource);
            var result = await commandService.Handle(command, cancellationToken);
            return MonitoringTelemetryActionResultAssembler.ToActionResult(this, result, errorLocalizer, problemDetailsFactory,
                r => Ok(TemperatureReadingResourceFromEntityAssembler.ToResourceFromEntity(r)));
        }
        
        // POST api/v1/monitoring/temperature/{assetId}/analyse/exhaust-limit
        [HttpPost("{assetId}/analyse/exhaust-limit")]
        [SwaggerOperation("AnalyseExhaustLimitPerCylinder", "Analyses exhaust temperature limit per cylinder.")]
        [ProducesResponseType(typeof(TemperatureReadingResource), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AnalyseExhaustLimit(
            string assetId, [FromBody] AnalyseExhaustLimitPerCylinderResource resource, CancellationToken cancellationToken)
        {
            var command = TemperatureReadingResourceFromEntityAssembler.AnalyseExhaustLimitCommandFromResourceAssembler.ToCommandFromResource(assetId, resource);
            var result = await commandService.Handle(command, cancellationToken);
            return MonitoringTelemetryActionResultAssembler.ToActionResult(this, result, errorLocalizer, problemDetailsFactory,
                r => Ok(TemperatureReadingResourceFromEntityAssembler.ToResourceFromEntity(r)));
        }
        
        // POST api/v1/monitoring/temperature/{assetId}/analyse/refrigerant
        [HttpPost("{assetId}/analyse/refrigerant")]
        [SwaggerOperation("AnalyseRefrigerantTemperature", "Analyses refrigerant temperature.")]
        [ProducesResponseType(typeof(TemperatureReadingResource), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AnalyseRefrigerant(
            string assetId, [FromBody] AnalyseRefrigerantTemperatureResource resource, CancellationToken cancellationToken)
        {
            var command = TemperatureReadingResourceFromEntityAssembler.AnalyseRefrigerantCommandFromResourceAssembler.ToCommandFromResource(assetId, resource);
            var result = await commandService.Handle(command, cancellationToken);
            return MonitoringTelemetryActionResultAssembler.ToActionResult(this, result, errorLocalizer, problemDetailsFactory,
                r => Ok(TemperatureReadingResourceFromEntityAssembler.ToResourceFromEntity(r)));
        }
        
        // POST api/v1/monitoring/temperature/{assetId}/analyse/oil
        [HttpPost("{assetId}/analyse/oil")]
        [SwaggerOperation("AnalyseOilTemperature", "Analyses engine oil temperature.")]
        [ProducesResponseType(typeof(TemperatureReadingResource), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AnalyseOil(
            string assetId, [FromBody] AnalyseOilTemperatureResource resource, CancellationToken cancellationToken)
        {
            var command = TemperatureReadingResourceFromEntityAssembler.AnalyseOilTemperatureCommandFromResourceAssembler.ToCommandFromResource(assetId, resource);
            var result = await commandService.Handle(command, cancellationToken);
            return MonitoringTelemetryActionResultAssembler.ToActionResult(this, result, errorLocalizer, problemDetailsFactory,
                r => Ok(TemperatureReadingResourceFromEntityAssembler.ToResourceFromEntity(r)));
        }
        
        // POST api/v1/monitoring/temperature/{assetId}/analyse/fuel
        [HttpPost("{assetId}/analyse/fuel")]
        [SwaggerOperation("AnalyseFuelTemperature", "Analyses engine fuel temperature.")]
        [ProducesResponseType(typeof(TemperatureReadingResource), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AnalyseFuel(
            string assetId, [FromBody] AnalyseFuelTemperatureResource resource, CancellationToken cancellationToken)
        {
            var command = TemperatureReadingResourceFromEntityAssembler.AnalyseFuelTemperatureCommandFromResourceAssembler.ToCommandFromResource(assetId, resource);
            var result = await commandService.Handle(command, cancellationToken);
            return MonitoringTelemetryActionResultAssembler.ToActionResult(this, result, errorLocalizer, problemDetailsFactory,
                r => Ok(TemperatureReadingResourceFromEntityAssembler.ToResourceFromEntity(r)));
        }
        
        // POST api/v1/monitoring/temperature/{assetId}/anomalies/detect
        [HttpPost("{assetId}/anomalies/detect")]
        [SwaggerOperation("DetectTemperatureAnomaly", "Detects and logs a temperature anomaly.")]
        [ProducesResponseType(typeof(TemperatureReadingResource), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DetectAnomaly(
            string assetId, [FromBody] DetectTemperatureAnomalyResource resource, CancellationToken cancellationToken)
        {
            var command = TemperatureReadingResourceFromEntityAssembler.DetectTemperatureAnomalyCommandFromResourceAssembler.ToCommandFromResource(assetId, resource);
            var result = await commandService.Handle(command, cancellationToken);
            return MonitoringTelemetryActionResultAssembler.ToActionResult(this, result, errorLocalizer, problemDetailsFactory,
                r => Ok(TemperatureReadingResourceFromEntityAssembler.ToResourceFromEntity(r)));
        }
    }