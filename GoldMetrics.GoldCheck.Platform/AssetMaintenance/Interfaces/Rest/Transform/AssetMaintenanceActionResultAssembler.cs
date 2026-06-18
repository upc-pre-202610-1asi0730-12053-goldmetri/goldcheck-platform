using GoldMetrics.GoldCheck.Platform.AssetMaintenance.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.AssetMaintenance.Domain.Model.Errors;
using GoldMetrics.GoldCheck.Platform.Shared.Application.Model;
using GoldMetrics.GoldCheck.Platform.Shared.Interfaces.Rest.ProblemDetails;
using GoldMetrics.GoldCheck.Platform.Shared.Resources.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace GoldMetrics.GoldCheck.Platform.AssetMaintenance.Interfaces.Rest.Transform;

public static class AssetMaintenanceActionResultAssembler
{
    private static int ToStatusCode(AssetMaintenanceError error) => error switch
    {
        AssetMaintenanceError.OperationCancelled => StatusCodes.Status409Conflict,
        AssetMaintenanceError.DatabaseError => StatusCodes.Status500InternalServerError,
        AssetMaintenanceError.InternalServerError => StatusCodes.Status500InternalServerError,
        _ => StatusCodes.Status400BadRequest
    };

    public static IActionResult ToActionResultFromMachineryResult(
        ControllerBase controller, Result<Machinery> result,
        IStringLocalizer<ErrorMessages> localizer, ProblemDetailsFactory factory,
        Func<Machinery, IActionResult> onSuccess)
    {
        if (result.IsSuccess) return onSuccess(result.Value!);
        return factory.CreateProblemDetails(controller, ToStatusCode((AssetMaintenanceError)result.Error!), result.Error, result.Message);
    }
}