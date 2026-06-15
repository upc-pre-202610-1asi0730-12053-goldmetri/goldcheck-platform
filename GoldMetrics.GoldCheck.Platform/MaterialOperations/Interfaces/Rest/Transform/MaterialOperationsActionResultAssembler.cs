using GoldMetrics.GoldCheck.Platform.MaterialOperations.Domain.Model;
using GoldMetrics.GoldCheck.Platform.MaterialOperations.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.Shared.Application.Model;
using GoldMetrics.GoldCheck.Platform.Shared.Interfaces.Rest.ProblemDetails;
using GoldMetrics.GoldCheck.Platform.Shared.Resources.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace GoldMetrics.GoldCheck.Platform.MaterialOperations.Interfaces.Rest.Transform;

public static class MaterialOperationsActionResultAssembler
{
    private static int ToStatusCode(MaterialOperationsError error) => error switch
    {
        MaterialOperationsError.MaterialNotFound => StatusCodes.Status404NotFound,
        MaterialOperationsError.InvalidMineralType => StatusCodes.Status400BadRequest,
        MaterialOperationsError.InvalidPayload => StatusCodes.Status400BadRequest,
        MaterialOperationsError.OperationCancelled => StatusCodes.Status409Conflict,
        MaterialOperationsError.DatabaseError => StatusCodes.Status500InternalServerError,
        MaterialOperationsError.InternalServerError => StatusCodes.Status500InternalServerError,
        _ => StatusCodes.Status400BadRequest
    };

    public static IActionResult ToActionResultFromMaterialResult(
        ControllerBase controller,
        Result<Material> result,
        IStringLocalizer<ErrorMessages> localizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<Material, IActionResult> onSuccess)
    {
        if (result.IsSuccess) return onSuccess(result.Value!);
        var statusCode = ToStatusCode((MaterialOperationsError)result.Error!);
        return problemDetailsFactory.CreateProblemDetails(controller, statusCode, result.Error, result.Message);
    }
}
