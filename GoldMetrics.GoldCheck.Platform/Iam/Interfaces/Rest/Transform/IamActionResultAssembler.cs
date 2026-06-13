using GoldMetrics.GoldCheck.Platform.Iam.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.Iam.Domain.Model.Errors;
using GoldMetrics.GoldCheck.Platform.Iam.Interfaces.Rest.Resources;
using GoldMetrics.GoldCheck.Platform.Shared.Application.Model;
using GoldMetrics.GoldCheck.Platform.Shared.Interfaces.Rest.ProblemDetails;
using Microsoft.AspNetCore.Mvc;

namespace GoldMetrics.GoldCheck.Platform.Iam.Interfaces.Rest.Transform;

public class IamActionResultAssembler(ProblemDetailsFactory problemDetailsFactory)
{
    private static int ToStatusCode(IamError error) => error switch
    {
        IamError.UserNotFound => StatusCodes.Status404NotFound,
        IamError.InvalidCredentials => StatusCodes.Status401Unauthorized,
        IamError.UsernameAlreadyExists => StatusCodes.Status409Conflict,
        IamError.EmailAlreadyRegistered => StatusCodes.Status409Conflict,
        IamError.InvalidRole => StatusCodes.Status400BadRequest,
        IamError.OperationCancelled => StatusCodes.Status409Conflict,
        IamError.DatabaseError => StatusCodes.Status500InternalServerError,
        IamError.InternalServerError => StatusCodes.Status500InternalServerError,
        _ => StatusCodes.Status400BadRequest
    };

    public IActionResult ToActionResult(Result<User> result, ControllerBase controller) =>
        result.IsSuccess
            ? controller.Ok(UserResourceFromEntityAssembler.ToResourceFromEntity(result.Value!))
            : problemDetailsFactory.CreateProblemDetails(controller, ToStatusCode((IamError)result.Error!), result.Error, result.Message);

    public IActionResult ToCreatedActionResult(Result<User> result, ControllerBase controller, string routeName, object routeValues) =>
        result.IsSuccess
            ? controller.CreatedAtRoute(routeName, routeValues, UserResourceFromEntityAssembler.ToResourceFromEntity(result.Value!))
            : problemDetailsFactory.CreateProblemDetails(controller, ToStatusCode((IamError)result.Error!), result.Error, result.Message);

    public IActionResult ToCollectionActionResult(Result<IEnumerable<User>> result, ControllerBase controller) =>
        result.IsSuccess
            ? controller.Ok(result.Value!.Select(UserResourceFromEntityAssembler.ToResourceFromEntity))
            : problemDetailsFactory.CreateProblemDetails(controller, ToStatusCode((IamError)result.Error!), result.Error, result.Message);

    public IActionResult ToTokenActionResult(Result<string> result, ControllerBase controller) =>
        result.IsSuccess
            ? controller.Ok(new AuthTokenResponse(result.Value!))
            : problemDetailsFactory.CreateProblemDetails(controller, ToStatusCode((IamError)result.Error!), result.Error, result.Message);
}

