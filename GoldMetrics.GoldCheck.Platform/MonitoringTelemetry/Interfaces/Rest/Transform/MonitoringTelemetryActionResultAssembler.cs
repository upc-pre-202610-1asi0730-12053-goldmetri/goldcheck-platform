using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Exceptions;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.Shared.Application.Model;
using GoldMetrics.GoldCheck.Platform.Shared.Resources.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using ProblemDetailsFactory =
    GoldMetrics.GoldCheck.Platform.Shared.Interfaces.Rest.ProblemDetails.ProblemDetailsFactory;

namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Interfaces.Rest.Transform;

public static class MonitoringTelemetryActionResultAssembler
{
    private static int ToStatusCode(MonitoringTelemetryError error) => error switch
    {
        MonitoringTelemetryError.AssetNotFound               => StatusCodes.Status404NotFound,
        MonitoringTelemetryError.TelemetryDataNotFound       => StatusCodes.Status404NotFound,
        MonitoringTelemetryError.TemperatureReadingNotFound  => StatusCodes.Status404NotFound,
        MonitoringTelemetryError.CommunicationChannelNotFound => StatusCodes.Status404NotFound,
        MonitoringTelemetryError.GNSSStatusNotFound          => StatusCodes.Status404NotFound,
        MonitoringTelemetryError.SpeedReadingNotFound        => StatusCodes.Status404NotFound,
        MonitoringTelemetryError.PressureReadingNotFound     => StatusCodes.Status404NotFound,
        MonitoringTelemetryError.InvalidTemperature          => StatusCodes.Status400BadRequest,
        MonitoringTelemetryError.InvalidSpeed                => StatusCodes.Status400BadRequest,
        MonitoringTelemetryError.InvalidPressure             => StatusCodes.Status400BadRequest,
        MonitoringTelemetryError.InvalidProtocol             => StatusCodes.Status400BadRequest,
        MonitoringTelemetryError.InvalidAnomalyType          => StatusCodes.Status400BadRequest,
        MonitoringTelemetryError.InvalidPressureType         => StatusCodes.Status400BadRequest,
        MonitoringTelemetryError.OperationCancelled          => StatusCodes.Status409Conflict,
        MonitoringTelemetryError.DatabaseError               => StatusCodes.Status500InternalServerError,
        _                                                    => StatusCodes.Status500InternalServerError
    };

    public static IActionResult ToActionResult<T>(
        ControllerBase controller,
        Result<T> result,
        IStringLocalizer<ErrorMessages> localizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<T, IActionResult> onSuccess)
    {
        if (result.IsSuccess) return onSuccess(result.Value!);
        var statusCode = ToStatusCode((MonitoringTelemetryError)result.Error!);
        return problemDetailsFactory.CreateProblemDetails(controller, statusCode, result.Error, result.Message);
    }
}