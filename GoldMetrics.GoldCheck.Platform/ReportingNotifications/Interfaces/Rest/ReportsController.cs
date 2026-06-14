using System.Net.Mime;
using GoldMetrics.GoldCheck.Platform.ReportingNotifications.Application.CommandServices;
using GoldMetrics.GoldCheck.Platform.ReportingNotifications.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.ReportingNotifications.Interfaces.Rest.Resources;
using GoldMetrics.GoldCheck.Platform.ReportingNotifications.Interfaces.Rest.Transform;
using GoldMetrics.GoldCheck.Platform.Shared.Interfaces.Rest.ProblemDetails;
using GoldMetrics.GoldCheck.Platform.Shared.Resources.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;

namespace GoldMetrics.GoldCheck.Platform.ReportingNotifications.Interfaces.Rest;

[ApiController]
[Route("api/v1/reports")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Report Endpoints.")]
public class ReportsController(
    IReportCommandService reportCommandService,
    IStringLocalizer<ErrorMessages> errorLocalizer,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    [HttpPost]
    [SwaggerOperation("Request Accident Data", "Create a new accident report request.", OperationId = "RequestAccidentData")]
    [SwaggerResponse(201, "Accident data requested.", typeof(ReportResource))]
    public async Task<IActionResult> RequestAccidentData(
        [FromBody] RequestAccidentDataResource resource,
        CancellationToken cancellationToken)
    {
        var command = new RequestAccidentDataCommand(
            resource.IncidentId,
            resource.SupervisorId);

        var result = await reportCommandService.Handle(command, cancellationToken);

        return ReportingNotificationsActionResultAssembler.ToActionResultFromReportResult(
            this,
            result,
            errorLocalizer,
            problemDetailsFactory,
            r => Created(string.Empty, ReportResourceFromEntityAssembler.ToResourceFromEntity(r)));
    }

    [HttpPut("{reportId:int}/load-data")]
    [SwaggerOperation("Load Accident Data", "Load accident data into the report.", OperationId = "LoadAccidentData")]
    [SwaggerResponse(200, "Accident data loaded.", typeof(ReportResource))]
    [SwaggerResponse(404, "Report not found.")]
    public async Task<IActionResult> LoadAccidentData(
        int reportId,
        CancellationToken cancellationToken)
    {
        var result = await reportCommandService.Handle(
            new LoadAccidentDataCommand(reportId),
            cancellationToken);

        return ReportingNotificationsActionResultAssembler.ToActionResultFromReportResult(
            this,
            result,
            errorLocalizer,
            problemDetailsFactory,
            r => Ok(ReportResourceFromEntityAssembler.ToResourceFromEntity(r)));
    }
}