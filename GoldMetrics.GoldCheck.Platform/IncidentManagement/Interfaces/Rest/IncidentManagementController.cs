using System.Net.Mime;
using GoldMetrics.GoldCheck.Platform.IncidentManagement.Application.CommandServices;
using GoldMetrics.GoldCheck.Platform.IncidentManagement.Application.QueryServices;
using GoldMetrics.GoldCheck.Platform.IncidentManagement.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.IncidentManagement.Domain.Model.Queries;
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
    IIncidentManagementQueryService queryService,
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
            r => CreatedAtAction(nameof(GetIncidentById),
                new { incidentId = r.Id },
                SafetyRecordResourceFromEntityAssembler.ToResourceFromEntity(r)));
    }
    
    [HttpGet("{incidentId:int}")]
    [SwaggerOperation("Get Incident By Id", "Get a safety record by incident identifier.", OperationId = "GetIncidentById")]
    [SwaggerResponse(200, "Incident found.", typeof(SafetyRecordResource))]
    [SwaggerResponse(404, "Incident not found.")]
    public async Task<IActionResult> GetIncidentById(int incidentId, CancellationToken cancellationToken)
    {
        var query = new GetIncidentByIdQuery(incidentId);
        var record = await queryService.Handle(query, cancellationToken);
        return IncidentManagementActionResultAssembler.ToActionResultFromGetSafetyRecordResult(
            this, record, errorLocalizer, problemDetailsFactory,
            r => Ok(SafetyRecordResourceFromEntityAssembler.ToResourceFromEntity(r)));
    }
    
    [HttpGet]
    [SwaggerOperation("Get All Incidents", "Get all safety incident records.", OperationId = "GetAllIncidents")]
    [SwaggerResponse(200, "Incidents found.", typeof(IEnumerable<SafetyRecordResource>))]
    public async Task<IActionResult> GetAllIncidents(CancellationToken cancellationToken)
    {
        var query = new GetAllIncidentsQuery();
        var records = await queryService.Handle(query, cancellationToken);
        return Ok(records.Select(SafetyRecordResourceFromEntityAssembler.ToResourceFromEntity));
    }
    
    [HttpPut("{incidentId:int}/escalate")]
    [SwaggerOperation("Escalate Risk Level", "Escalate the risk level of an incident.", OperationId = "EscalateRiskLevel")]
    [SwaggerResponse(200, "Risk level escalated.", typeof(SafetyRecordResource))]
    [SwaggerResponse(400, "Invalid risk level.")]
    [SwaggerResponse(404, "Incident not found.")]
    public async Task<IActionResult> EscalateRiskLevel(int incidentId, [FromBody] EscalateRiskLevelResource resource, CancellationToken cancellationToken)
    {
        var command = new EscalateRiskLevelCommand(incidentId, resource.NewRiskLevel);
        var result = await commandService.Handle(command, cancellationToken);
        return IncidentManagementActionResultAssembler.ToActionResultFromSafetyRecordResult(
            this, result, errorLocalizer, problemDetailsFactory,
            r => Ok(SafetyRecordResourceFromEntityAssembler.ToResourceFromEntity(r)));
    }
    
    [HttpPut("{incidentId:int}/evaluate")]
    [SwaggerOperation("Evaluate Safety Risk", "Evaluate and update the safety risk level.", OperationId = "EvaluateSafetyRisk")]
    [SwaggerResponse(200, "Risk level updated.", typeof(SafetyRecordResource))]
    [SwaggerResponse(404, "Incident not found.")]
    public async Task<IActionResult> EvaluateSafetyRisk(int incidentId, CancellationToken cancellationToken)
    {
        var command = new EvaluateSafetyRiskCommand(incidentId);
        var result = await commandService.Handle(command, cancellationToken);
        return IncidentManagementActionResultAssembler.ToActionResultFromSafetyRecordResult(
            this, result, errorLocalizer, problemDetailsFactory,
            r => Ok(SafetyRecordResourceFromEntityAssembler.ToResourceFromEntity(r)));
    }
    
    [HttpPut("{incidentId:int}/alert")]
    [SwaggerOperation("Send Risk Level Alert", "Send a risk level alert for an incident.", OperationId = "SendRiskLevelAlert")]
    [SwaggerResponse(200, "Risk level alert committed.", typeof(SafetyRecordResource))]
    [SwaggerResponse(404, "Incident not found.")]
    public async Task<IActionResult> SendRiskLevelAlert(int incidentId, CancellationToken cancellationToken)
    {
        var command = new SendRiskLevelAlertCommand(incidentId);
        var result = await commandService.Handle(command, cancellationToken);
        return IncidentManagementActionResultAssembler.ToActionResultFromSafetyRecordResult(
            this, result, errorLocalizer, problemDetailsFactory,
            r => Ok(SafetyRecordResourceFromEntityAssembler.ToResourceFromEntity(r)));
    }
}