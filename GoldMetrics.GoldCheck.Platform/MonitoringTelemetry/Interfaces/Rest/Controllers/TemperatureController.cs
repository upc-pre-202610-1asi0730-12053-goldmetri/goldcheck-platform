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
    }