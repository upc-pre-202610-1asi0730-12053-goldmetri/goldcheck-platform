using System.Net.Mime;
using GoldMetrics.GoldCheck.Platform.ReportingNotifications.Application.CommandServices;
using GoldMetrics.GoldCheck.Platform.ReportingNotifications.Application.QueryServices;
using GoldMetrics.GoldCheck.Platform.ReportingNotifications.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.ReportingNotifications.Domain.Model.Queries;
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
    IReportQueryService reportQueryService,
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
            r => CreatedAtAction(nameof(GetReportById), new { reportId = r.Id },
                ReportResourceFromEntityAssembler.ToResourceFromEntity(r)));
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
    
    [HttpPut("{reportId:int}/generate")]
    [SwaggerOperation("Generate Report", "Generate the accident report.", OperationId = "GenerateReport")]
    [SwaggerResponse(200, "Report generated.", typeof(ReportResource))]
    [SwaggerResponse(404, "Report not found.")]
    public async Task<IActionResult> GenerateReport(int reportId, CancellationToken cancellationToken)
    {
        var result = await reportCommandService.Handle(new GenerateReportCommand(reportId), cancellationToken);
        return ReportingNotificationsActionResultAssembler.ToActionResultFromReportResult(
            this, result, errorLocalizer, problemDetailsFactory,
            r => Ok(ReportResourceFromEntityAssembler.ToResourceFromEntity(r)));
    }
    
    [HttpPut("{reportId:int}/request-export")]
    [SwaggerOperation("Request Report Exportation", "Request exportation of the report in a specific format.", OperationId = "RequestReportExportation")]
    [SwaggerResponse(200, "Exportation requested.", typeof(ReportResource))]
    [SwaggerResponse(400, "Invalid report format.")]
    [SwaggerResponse(404, "Report not found.")]
    public async Task<IActionResult> RequestReportExportation(int reportId, [FromBody] RequestReportExportationResource resource, CancellationToken cancellationToken)
    {
        var result = await reportCommandService.Handle(new RequestReportExportationCommand(reportId, resource.Format), cancellationToken);
        return ReportingNotificationsActionResultAssembler.ToActionResultFromReportResult(
            this, result, errorLocalizer, problemDetailsFactory,
            r => Ok(ReportResourceFromEntityAssembler.ToResourceFromEntity(r)));
    }
    
    [HttpPut("{reportId:int}/export")]
    [SwaggerOperation("Export Report", "Export the report.", OperationId = "ExportReport")]
    [SwaggerResponse(200, "Report exported.", typeof(ReportResource))]
    [SwaggerResponse(404, "Report not found.")]
    public async Task<IActionResult> ExportReport(int reportId, CancellationToken cancellationToken)
    {
        var result = await reportCommandService.Handle(new ExportReportCommand(reportId), cancellationToken);
        return ReportingNotificationsActionResultAssembler.ToActionResultFromReportResult(
            this, result, errorLocalizer, problemDetailsFactory,
            r => Ok(ReportResourceFromEntityAssembler.ToResourceFromEntity(r)));
    }
    
    [HttpGet("{reportId:int}/download")]
    [SwaggerOperation("Download Report", "Download a generated and exported report.", OperationId = "DownloadReport")]
    [SwaggerResponse(200, "Report downloaded.", typeof(ReportResource))]
    [SwaggerResponse(404, "Report not found.")]
    public async Task<IActionResult> DownloadReport(int reportId, [FromQuery] string userId, CancellationToken cancellationToken)
    {
        var result = await reportCommandService.Handle(new DownloadReportCommand(reportId, userId), cancellationToken);
        return ReportingNotificationsActionResultAssembler.ToActionResultFromReportResult(
            this, result, errorLocalizer, problemDetailsFactory,
            r => Ok(ReportResourceFromEntityAssembler.ToResourceFromEntity(r)));
    }
    
    [HttpGet("{reportId:int}")]
    [SwaggerOperation("Get Report By Id", "Get a report by its identifier.", OperationId = "GetReportById")]
    [SwaggerResponse(200, "Report found.", typeof(ReportResource))]
    [SwaggerResponse(404, "Report not found.")]
    public async Task<IActionResult> GetReportById(int reportId, CancellationToken cancellationToken)
    {
        var report = await reportQueryService.Handle(new GetReportByIdQuery(reportId), cancellationToken);
        return ReportingNotificationsActionResultAssembler.ToActionResultFromGetReportResult(
            this, report, errorLocalizer, problemDetailsFactory,
            r => Ok(ReportResourceFromEntityAssembler.ToResourceFromEntity(r)));
    }
    
    [HttpGet]
    [SwaggerOperation("Get All Reports", "Get all accident reports.", OperationId = "GetAllReports")]
    [SwaggerResponse(200, "Reports found.", typeof(IEnumerable<ReportResource>))]
    public async Task<IActionResult> GetAllReports(CancellationToken cancellationToken)
    {
        var reports = await reportQueryService.Handle(new GetAllReportsQuery(), cancellationToken);
        return Ok(reports.Select(ReportResourceFromEntityAssembler.ToResourceFromEntity));
    }
}