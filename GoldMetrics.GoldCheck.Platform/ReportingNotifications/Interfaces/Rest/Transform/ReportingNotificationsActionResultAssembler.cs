using GoldMetrics.GoldCheck.Platform.ReportingNotifications.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.ReportingNotifications.Domain.Model.Errors;
using GoldMetrics.GoldCheck.Platform.Shared.Application.Model;
using GoldMetrics.GoldCheck.Platform.Shared.Interfaces.Rest.ProblemDetails;
using GoldMetrics.GoldCheck.Platform.Shared.Resources.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace GoldMetrics.GoldCheck.Platform.ReportingNotifications.Interfaces.Rest.Transform;

public static class ReportingNotificationsActionResultAssembler
{
    private static int ToStatusCode(ReportingNotificationsError error) => error switch
    {
        ReportingNotificationsError.ReportNotFound => StatusCodes.Status404NotFound,
        ReportingNotificationsError.InvalidReportFormat => StatusCodes.Status400BadRequest,
        ReportingNotificationsError.AccidentValidationFailed => StatusCodes.Status422UnprocessableEntity,
        ReportingNotificationsError.OperationCancelled => StatusCodes.Status409Conflict,
        ReportingNotificationsError.DatabaseError => StatusCodes.Status500InternalServerError,
        ReportingNotificationsError.InternalServerError => StatusCodes.Status500InternalServerError,
        _ => StatusCodes.Status400BadRequest
    };

    public static IActionResult ToActionResultFromReportResult(
        ControllerBase controller, Result<Report> result,
        IStringLocalizer<ErrorMessages> localizer, ProblemDetailsFactory factory,
        Func<Report, IActionResult> onSuccess)
    {
        if (result.IsSuccess) return onSuccess(result.Value!);
        return factory.CreateProblemDetails(controller, ToStatusCode((ReportingNotificationsError)result.Error!), result.Error, result.Message);
    }
}