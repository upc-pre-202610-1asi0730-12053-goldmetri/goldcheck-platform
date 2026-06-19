using GoldMetrics.GoldCheck.Platform.FleetOperations.Domain.Model;
using GoldMetrics.GoldCheck.Platform.FleetOperations.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.Shared.Application.Model;
using GoldMetrics.GoldCheck.Platform.Shared.Interfaces.Rest.ProblemDetails;
using GoldMetrics.GoldCheck.Platform.Shared.Resources.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace GoldMetrics.GoldCheck.Platform.FleetOperations.Interfaces.Rest.Transform;

public static class FleetOperationsActionResultAssembler
{
    private static int ToStatusCode(FleetOperationsError error) => error switch
    {
        FleetOperationsError.VehicleNotFound => StatusCodes.Status404NotFound,
        FleetOperationsError.HaulingCycleNotFound => StatusCodes.Status404NotFound,
        FleetOperationsError.VehicleAlreadyAssigned => StatusCodes.Status409Conflict,
        FleetOperationsError.InvalidPayload => StatusCodes.Status400BadRequest,
        FleetOperationsError.OperationCancelled => StatusCodes.Status409Conflict,
        FleetOperationsError.DatabaseError => StatusCodes.Status500InternalServerError,
        FleetOperationsError.InternalServerError => StatusCodes.Status500InternalServerError,
        _ => StatusCodes.Status400BadRequest
    };

    public static IActionResult ToActionResultFromVehicleResult(
        ControllerBase controller,
        Result<Vehicle> result,
        IStringLocalizer<ErrorMessages> localizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<Vehicle, IActionResult> onSuccess)
    {
        if (result.IsSuccess) return onSuccess(result.Value!);
        var statusCode = ToStatusCode((FleetOperationsError)result.Error!);
        return problemDetailsFactory.CreateProblemDetails(controller, statusCode, result.Error, result.Message);
    }

    public static IActionResult ToActionResultFromGetVehicleResult(
        ControllerBase controller,
        Vehicle? vehicle,
        IStringLocalizer<ErrorMessages> localizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<Vehicle, IActionResult> onSuccess)
    {
        if (vehicle is null)
            return problemDetailsFactory.CreateProblemDetails(
                controller,
                ToStatusCode(FleetOperationsError.VehicleNotFound),
                FleetOperationsError.VehicleNotFound,
                localizer[nameof(FleetOperationsError.VehicleNotFound)]);
        return onSuccess(vehicle);
    }

    public static IActionResult ToActionResultFromHaulingCycleResult(
        ControllerBase controller,
        Result<HaulingCycle> result,
        IStringLocalizer<ErrorMessages> localizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<HaulingCycle, IActionResult> onSuccess)
    {
        if (result.IsSuccess) return onSuccess(result.Value!);
        var statusCode = ToStatusCode((FleetOperationsError)result.Error!);
        return problemDetailsFactory.CreateProblemDetails(controller, statusCode, result.Error, result.Message);
    }

    public static IActionResult ToActionResultFromGetHaulingCycleResult(
        ControllerBase controller,
        HaulingCycle? cycle,
        IStringLocalizer<ErrorMessages> localizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<HaulingCycle, IActionResult> onSuccess)
    {
        if (cycle is null)
            return problemDetailsFactory.CreateProblemDetails(
                controller,
                ToStatusCode(FleetOperationsError.HaulingCycleNotFound),
                FleetOperationsError.HaulingCycleNotFound,
                localizer[nameof(FleetOperationsError.HaulingCycleNotFound)]);
        return onSuccess(cycle);
    }
}
