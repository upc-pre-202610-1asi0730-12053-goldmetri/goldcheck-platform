using GoldMetrics.GoldCheck.Platform.Analytics.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.Analytics.Domain.Model.Errors;
using GoldMetrics.GoldCheck.Platform.Shared.Application.Model;
using GoldMetrics.GoldCheck.Platform.Shared.Interfaces.Rest.ProblemDetails;
using GoldMetrics.GoldCheck.Platform.Shared.Resources.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace GoldMetrics.GoldCheck.Platform.Analytics.Interfaces.Rest.Transform;

public static class AnalyticsActionResultAssembler
{
    private static int ToStatusCode(AnalyticsError error) => error switch
    {
        AnalyticsError.MaterialNotFound => StatusCodes.Status404NotFound,
        AnalyticsError.RouteNotFound => StatusCodes.Status404NotFound,
        AnalyticsError.OperationCancelled => StatusCodes.Status409Conflict,
        AnalyticsError.DatabaseError => StatusCodes.Status500InternalServerError,
        AnalyticsError.InternalServerError => StatusCodes.Status500InternalServerError,
        AnalyticsError.InvalidProductionPeriod => StatusCodes.Status400BadRequest,
        AnalyticsError.InsufficientData => StatusCodes.Status422UnprocessableEntity,
        AnalyticsError.ProductionDataValidationFailed => StatusCodes.Status422UnprocessableEntity,
        _ => StatusCodes.Status400BadRequest
        
    };

    public static IActionResult ToActionResultFromMaterialResult(
        ControllerBase controller, Result<Material> result,
        IStringLocalizer<ErrorMessages> localizer, ProblemDetailsFactory problemDetailsFactory,
        Func<Material, IActionResult> onSuccess)
    {
        if (result.IsSuccess) return onSuccess(result.Value!);
        var statusCode = ToStatusCode((AnalyticsError)result.Error!);
        return problemDetailsFactory.CreateProblemDetails(controller, statusCode, result.Error, result.Message);
    }
    
    public static IActionResult ToActionResultFromGetMaterialResult(
        ControllerBase controller, Material? material,
        IStringLocalizer<ErrorMessages> localizer, ProblemDetailsFactory problemDetailsFactory,
        Func<Material, IActionResult> onSuccess)
    {
        if (material is null)
            return problemDetailsFactory.CreateProblemDetails(
                controller, ToStatusCode(AnalyticsError.MaterialNotFound),
                AnalyticsError.MaterialNotFound, localizer[nameof(AnalyticsError.MaterialNotFound)]);
        return onSuccess(material);
    }
}