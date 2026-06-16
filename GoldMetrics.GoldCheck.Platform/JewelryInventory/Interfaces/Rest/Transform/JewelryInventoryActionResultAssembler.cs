using GoldMetrics.GoldCheck.Platform.JewelryInventory.Domain.Exceptions;
using GoldMetrics.GoldCheck.Platform.JewelryInventory.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.Shared.Application.Model;
using GoldMetrics.GoldCheck.Platform.Shared.Resources.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using ProblemDetailsFactory =
    GoldMetrics.GoldCheck.Platform.Shared.Interfaces.Rest.ProblemDetails.ProblemDetailsFactory;

namespace GoldMetrics.GoldCheck.Platform.JewelryInventory.Interfaces.Rest.Transform;

public static class JewelryInventoryActionResultAssembler
{
    private static int ToStatusCode(JewelryInventoryError error) => error switch
    {
        JewelryInventoryError.MaterialNotFound         => StatusCodes.Status404NotFound,
        JewelryInventoryError.CertificateNotFound      => StatusCodes.Status404NotFound,
        JewelryInventoryError.MaterialAlreadyCertified => StatusCodes.Status409Conflict,
        JewelryInventoryError.InvalidQRCode            => StatusCodes.Status400BadRequest,
        JewelryInventoryError.InvalidStatus            => StatusCodes.Status400BadRequest,
        JewelryInventoryError.OperationCancelled       => StatusCodes.Status409Conflict,
        JewelryInventoryError.DatabaseError            => StatusCodes.Status500InternalServerError,
        _                                              => StatusCodes.Status500InternalServerError
    };

    public static IActionResult ToActionResultFromMaterialResult(
        ControllerBase controller,
        Result<JewelryMaterial> result,
        IStringLocalizer<ErrorMessages> localizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<JewelryMaterial, IActionResult> onSuccess)
    {
        if (result.IsSuccess) return onSuccess(result.Value!);

        var statusCode = ToStatusCode((JewelryInventoryError)result.Error!);
        return problemDetailsFactory.CreateProblemDetails(
            controller, statusCode, result.Error, result.Message);
    }

    public static IActionResult ToActionResultFromJewelryResult(
        ControllerBase controller,
        Result<Jewelry> result,
        IStringLocalizer<ErrorMessages> localizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<Jewelry, IActionResult> onSuccess)
    {
        if (result.IsSuccess) return onSuccess(result.Value!);

        var statusCode = ToStatusCode((JewelryInventoryError)result.Error!);
        return problemDetailsFactory.CreateProblemDetails(
            controller, statusCode, result.Error, result.Message);
    }
}
