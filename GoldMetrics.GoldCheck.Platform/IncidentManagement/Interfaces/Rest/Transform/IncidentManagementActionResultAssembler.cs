using GoldMetrics.GoldCheck.Platform.IncidentManagement.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.IncidentManagement.Domain.Model.Errors;
using GoldMetrics.GoldCheck.Platform.Shared.Application.Model;
using GoldMetrics.GoldCheck.Platform.Shared.Interfaces.Rest.ProblemDetails;
using GoldMetrics.GoldCheck.Platform.Shared.Resources.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace GoldMetrics.GoldCheck.Platform.IncidentManagement.Interfaces.Rest.Transform;

public static class IncidentManagementActionResultAssembler
{
    private static int ToStatusCode(IncidentManagementError error) => error switch
    {
        IncidentManagementError.IncidentNotFound => StatusCodes.Status404NotFound,
        IncidentManagementError.InvalidRiskLevel => StatusCodes.Status400BadRequest,
        IncidentManagementError.InvalidIncidentType => StatusCodes.Status400BadRequest,
        IncidentManagementError.IncidentAlreadyCommitted => StatusCodes.Status409Conflict,
        IncidentManagementError.RiskLevelValidationFailed => StatusCodes.Status422UnprocessableEntity,
        IncidentManagementError.OperationCancelled => StatusCodes.Status409Conflict,
        IncidentManagementError.DatabaseError => StatusCodes.Status500InternalServerError,
        IncidentManagementError.InternalServerError => StatusCodes.Status500InternalServerError,
        _ => StatusCodes.Status400BadRequest
    };

    public static IActionResult ToActionResultFromSafetyRecordResult(
        ControllerBase controller,
        Result<SafetyRecord> result,
        IStringLocalizer<ErrorMessages> localizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<SafetyRecord, IActionResult> onSuccess)
    {
        if (result.IsSuccess) return onSuccess(result.Value!);
        var statusCode = ToStatusCode((IncidentManagementError)result.Error!);
        return problemDetailsFactory.CreateProblemDetails(controller, statusCode, result.Error, result.Message);
    }
    public static IActionResult ToActionResultFromGetSafetyRecordResult(
        ControllerBase controller,
        SafetyRecord? record,
        IStringLocalizer<ErrorMessages> localizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<SafetyRecord, IActionResult> onSuccess)
    {
        if (record is null)
            return problemDetailsFactory.CreateProblemDetails(
                controller,
                ToStatusCode(IncidentManagementError.IncidentNotFound),
                IncidentManagementError.IncidentNotFound,
                localizer[nameof(IncidentManagementError.IncidentNotFound)]);
        return onSuccess(record);
    }
}