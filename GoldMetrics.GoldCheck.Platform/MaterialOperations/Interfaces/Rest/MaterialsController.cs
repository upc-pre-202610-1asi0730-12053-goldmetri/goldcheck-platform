using System.Net.Mime;
using GoldMetrics.GoldCheck.Platform.MaterialOperations.Application.CommandServices;
using GoldMetrics.GoldCheck.Platform.MaterialOperations.Interfaces.Rest.Resources;
using GoldMetrics.GoldCheck.Platform.MaterialOperations.Interfaces.Rest.Transform;
using GoldMetrics.GoldCheck.Platform.Shared.Interfaces.Rest.ProblemDetails;
using GoldMetrics.GoldCheck.Platform.Shared.Resources.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;

namespace GoldMetrics.GoldCheck.Platform.MaterialOperations.Interfaces.Rest;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Material Endpoints.")]
public class MaterialsController(
    IMaterialCommandService materialCommandService,
    IStringLocalizer<ErrorMessages> errorLocalizer,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    [HttpPost]
    [SwaggerOperation("Identify Mineral Type", "Identify and register a new material batch.", OperationId = "IdentifyMineralType")]
    [SwaggerResponse(201, "Material batch identified.", typeof(MaterialResource))]
    [SwaggerResponse(400, "Invalid mineral type or payload.")]
    public async Task<IActionResult> IdentifyMineralType([FromBody] CreateMaterialResource resource, CancellationToken cancellationToken)
    {
        var command = IdentifyMineralTypeCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await materialCommandService.Handle(command, cancellationToken);

        return MaterialOperationsActionResultAssembler.ToActionResultFromMaterialResult(
            this, result, errorLocalizer, problemDetailsFactory,
            material => Created(string.Empty, MaterialResourceFromEntityAssembler.ToResourceFromEntity(material)));
    }
}
