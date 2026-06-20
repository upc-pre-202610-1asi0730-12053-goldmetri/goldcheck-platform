using System.Net.Mime;
using GoldMetrics.GoldCheck.Platform.IncidentManagement.Application.CommandServices;
using GoldMetrics.GoldCheck.Platform.IncidentManagement.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.IncidentManagement.Interfaces.Rest.Resources;
using GoldMetrics.GoldCheck.Platform.IncidentManagement.Interfaces.Rest.Transform;
using GoldMetrics.GoldCheck.Platform.Shared.Interfaces.Rest.ProblemDetails;
using GoldMetrics.GoldCheck.Platform.Shared.Resources.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;

namespace GoldMetrics.GoldCheck.Platform.IncidentManagement.Interfaces.Rest;

[ApiController]
[Route("api/v1/incidents")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Incident Management Endpoints.")]
public class IncidentManagementController(
    IIncidentManagementCommandService commandService,
    IStringLocalizer<ErrorMessages> errorLocalizer,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    [HttpPost("fatigue")]
    [SwaggerOperation("Detect Driver Fatigue", "Create a new fatigue incident record.", OperationId = "DetectDriverFatigue")]
    [SwaggerResponse(201, "Fatigue event triggered.", typeof(SafetyRecordResource))]
    [SwaggerResponse(400, "Invalid request data.")]
    public async Task<IActionResult> DetectDriverFatigue([FromBody] DetectDriverFatigueResource resource, CancellationToken cancellationToken)
    {
        var command = new DetectDriverFatigueCommand(resource.OperatorId, resource.AssetId);
        var result = await commandService.Handle(command, cancellationToken);
        return IncidentManagementActionResultAssembler.ToActionResultFromSafetyRecordResult(
            this, result, errorLocalizer, problemDetailsFactory,
            r => Created(string.Empty, SafetyRecordResourceFromEntityAssembler.ToResourceFromEntity(r)));
    }
}