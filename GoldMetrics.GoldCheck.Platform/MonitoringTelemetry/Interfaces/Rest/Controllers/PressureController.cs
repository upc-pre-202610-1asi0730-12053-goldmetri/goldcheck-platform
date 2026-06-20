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
    }