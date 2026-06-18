using System.Net.Mime;
using GoldMetrics.GoldCheck.Platform.Analytics.Application.CommandServices;
using GoldMetrics.GoldCheck.Platform.Analytics.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.Analytics.Interfaces.Rest.Resources;
using GoldMetrics.GoldCheck.Platform.Analytics.Interfaces.Rest.Transform;
using GoldMetrics.GoldCheck.Platform.Shared.Interfaces.Rest.ProblemDetails;
using GoldMetrics.GoldCheck.Platform.Shared.Resources.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;

namespace GoldMetrics.GoldCheck.Platform.Analytics.Interfaces.Rest;

[ApiController]
[Route("api/v1/analytics")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Analytics Endpoints.")]
public class AnalyticsController(
    IAnalyticsCommandService analyticsCommandService,
    IStringLocalizer<ErrorMessages> errorLocalizer,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    [HttpPost("routes/view")]
    [SwaggerOperation("View Route Progress", "Load and view route progress data.", OperationId = "ViewRouteProgress")]
    [SwaggerResponse(201, "Route data loaded.", typeof(MaterialResource))]
    [SwaggerResponse(400, "Invalid request data.")]
    public async Task<IActionResult> ViewRouteProgress([FromBody] ViewRouteProgressResource resource, CancellationToken cancellationToken)
    {
        var command = new ViewRouteProgressCommand(resource.RouteId, resource.UserId);
        var result = await analyticsCommandService.Handle(command, cancellationToken);
        return AnalyticsActionResultAssembler.ToActionResultFromMaterialResult(
            this, result, errorLocalizer, problemDetailsFactory,
            m => Created(string.Empty, MaterialResourceFromEntityAssembler.ToResourceFromEntity(m)));
    }
}