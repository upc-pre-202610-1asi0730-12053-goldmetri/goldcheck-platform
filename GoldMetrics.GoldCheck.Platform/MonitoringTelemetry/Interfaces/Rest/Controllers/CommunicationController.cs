using System.Net.Mime;
    using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Application.CommandServices;
    using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Application.QueryServices;
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
    [Route("api/v1/monitoring/communication")]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerTag("Available Monitoring & Telemetry — Communication Endpoints.")]
    public class CommunicationController(
        ICommunicationCommandService commandService,
        ICommunicationQueryService queryService,
        IStringLocalizer<ErrorMessages> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory)
        : ControllerBase
    {
        // POST api/v1/monitoring/communication/monitor
        [HttpPost("monitor")]
        [SwaggerOperation("MonitorCommunicationChannel", "Starts communication channel monitoring for an asset.")]
        [ProducesResponseType(typeof(CommunicationChannelResource), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> MonitorChannel(
            [FromBody] MonitorCommunicationChannelResource resource, CancellationToken cancellationToken)
        {
            var command = MonitorCommunicationCommandFromResourceAssembler.ToCommandFromResource(resource);
            var result = await commandService.Handle(command, cancellationToken);
            return MonitoringTelemetryActionResultAssembler.ToActionResult(this, result, errorLocalizer, problemDetailsFactory,
                c => CreatedAtAction(nameof(GetByAsset), new { assetId = c.AssetId.Value },
                    CommunicationChannelResourceFromEntityAssembler.ToResourceFromEntity(c)));
        }
        
        // POST api/v1/monitoring/communication/{assetId}/analyse
        [HttpPost("{assetId}/analyse")]
        [SwaggerOperation("AnalyseCommunication", "Analyses a communication channel for an asset.")]
        [ProducesResponseType(typeof(CommunicationChannelResource), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Analyse(
            string assetId, [FromBody] AnalyseCommunicationResource resource, CancellationToken cancellationToken)
        {
            var command = CommunicationChannelResourceFromEntityAssembler.AnalyseCommunicationCommandFromResourceAssembler.ToCommandFromResource(assetId, resource);
            var result = await commandService.Handle(command, cancellationToken);
            return MonitoringTelemetryActionResultAssembler.ToActionResult(this, result, errorLocalizer, problemDetailsFactory,
                c => Ok(CommunicationChannelResourceFromEntityAssembler.ToResourceFromEntity(c)));
        }
        
        // POST api/v1/monitoring/communication/{assetId}/anomalies/detect
        [HttpPost("{assetId}/anomalies/detect")]
        [SwaggerOperation("DetectCommunicationAnomaly", "Detects and logs a communication anomaly.")]
        [ProducesResponseType(typeof(CommunicationChannelResource), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DetectAnomaly(
            string assetId, [FromBody] DetectCommunicationAnomalyResource resource, CancellationToken cancellationToken)
        {
            var command = CommunicationChannelResourceFromEntityAssembler.DetectCommunicationAnomalyCommandFromResourceAssembler.ToCommandFromResource(assetId, resource);
            var result = await commandService.Handle(command, cancellationToken);
            return MonitoringTelemetryActionResultAssembler.ToActionResult(this, result, errorLocalizer, problemDetailsFactory,
                c => Ok(CommunicationChannelResourceFromEntityAssembler.ToResourceFromEntity(c)));
        }
        
        // GET api/v1/monitoring/communication/{assetId}
        [HttpGet("{assetId}")]
        [SwaggerOperation("GetCommunicationChannelsByAsset", "Returns communication channel records for an asset.")]
        [ProducesResponseType(typeof(IEnumerable<CommunicationChannelResource>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByAsset(string assetId, CancellationToken cancellationToken)
        {
            var channels = await queryService.Handle(new GetCommunicationChannelByAssetQuery(assetId), cancellationToken);
            return Ok(channels.Select(CommunicationChannelResourceFromEntityAssembler.ToResourceFromEntity));
        }
    }