using System.Net.Mime;
using GoldMetrics.GoldCheck.Platform.Analytics.Application.CommandServices;
using GoldMetrics.GoldCheck.Platform.Analytics.Application.QueryServices;
using GoldMetrics.GoldCheck.Platform.Analytics.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.Analytics.Domain.Model.Queries;
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
    IAnalyticsQueryService analyticsQueryService,
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
            m => CreatedAtAction(nameof(GetRouteProgressById),
                new { routeId = m.RouteId.Value },
                MaterialResourceFromEntityAssembler.ToResourceFromEntity(m)));
    }
    
    [HttpGet("routes/{routeId}")]
    [SwaggerOperation("Get Route Progress By Id", "Get route progress data by route identifier.", OperationId = "GetRouteProgressById")]
    [SwaggerResponse(200, "Route data found.", typeof(MaterialResource))]
    [SwaggerResponse(404, "Route not found.")]
    public async Task<IActionResult> GetRouteProgressById(string routeId, CancellationToken cancellationToken)
    {
        var query = new GetRouteProgressByIdQuery(routeId);
        var material = await analyticsQueryService.Handle(query, cancellationToken);
        return AnalyticsActionResultAssembler.ToActionResultFromGetMaterialResult(
            this, material, errorLocalizer, problemDetailsFactory,
            m => Ok(MaterialResourceFromEntityAssembler.ToResourceFromEntity(m)));
    }
    
    [HttpGet("routes")]
    [SwaggerOperation("Get All Routes", "Get all route progress records.", OperationId = "GetAllRoutes")]
    [SwaggerResponse(200, "Routes found.", typeof(IEnumerable<MaterialResource>))]
    public async Task<IActionResult> GetAllRoutes(CancellationToken cancellationToken)
    {
        var query = new GetAllRoutesQuery();
        var materials = await analyticsQueryService.Handle(query, cancellationToken);
        return Ok(materials.Select(MaterialResourceFromEntityAssembler.ToResourceFromEntity));
    }
}