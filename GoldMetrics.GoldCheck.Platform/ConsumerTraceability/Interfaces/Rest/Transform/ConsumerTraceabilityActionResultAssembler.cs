using GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Domain.Exceptions;
using GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.Shared.Application.Model;
using GoldMetrics.GoldCheck.Platform.Shared.Resources.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using ProblemDetailsFactory =
    GoldMetrics.GoldCheck.Platform.Shared.Interfaces.Rest.ProblemDetails.ProblemDetailsFactory;

namespace GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Interfaces.Rest.Transform;

public static class ConsumerTraceabilityActionResultAssembler
{
    private static int ToStatusCode(ConsumerTraceabilityError error) => error switch
    {
        ConsumerTraceabilityError.ProductNotFound    => StatusCodes.Status404NotFound,
        ConsumerTraceabilityError.InvalidQRCode      => StatusCodes.Status400BadRequest,
        ConsumerTraceabilityError.OperationCancelled => StatusCodes.Status409Conflict,
        ConsumerTraceabilityError.DatabaseError      => StatusCodes.Status500InternalServerError,
        _                                            => StatusCodes.Status500InternalServerError
    };

    public static IActionResult ToActionResultFromProductResult(
        ControllerBase controller,
        Result<JewelryProduct> result,
        IStringLocalizer<ErrorMessages> localizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<JewelryProduct, IActionResult> onSuccess)
    {
        if (result.IsSuccess) return onSuccess(result.Value!);
        var statusCode = ToStatusCode((ConsumerTraceabilityError)result.Error!);
        return problemDetailsFactory.CreateProblemDetails(
            controller, statusCode, result.Error, result.Message);
    }
}