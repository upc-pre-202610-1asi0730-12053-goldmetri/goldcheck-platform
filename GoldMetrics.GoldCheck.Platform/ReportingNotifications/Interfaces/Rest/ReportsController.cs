using GoldMetrics.GoldCheck.Platform.ReportingNotifications.Domain.Model.Commands;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GoldMetrics.GoldCheck.Platform.ReportingNotifications.Interfaces.Rest;

public class ReportsController
{
    [HttpPut("{reportId:int}/load-data")]
    [SwaggerOperation("Load Accident Data", "Load accident data into the report.", OperationId = "LoadAccidentData")]
    [SwaggerResponse(200, "Accident data loaded.", typeof(ReportResource))]
    [SwaggerResponse(404, "Report not found.")]
    public async Task<IActionResult> LoadAccidentData(int reportId, CancellationToken cancellationToken)
    {
        var result = await reportCommandService.Handle(new LoadAccidentDataCommand(reportId), cancellationToken);
        return ReportingNotificationsActionResultAssembler.ToActionResultFromReportResult(
            this, result, errorLocalizer, problemDetailsFactory,
            r => Ok(ReportResourceFromEntityAssembler.ToResourceFromEntity(r)));
    }
}