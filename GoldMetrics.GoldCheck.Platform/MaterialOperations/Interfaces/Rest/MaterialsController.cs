using System.Net.Mime;
using GoldMetrics.GoldCheck.Platform.MaterialOperations.Application.CommandServices;
using GoldMetrics.GoldCheck.Platform.MaterialOperations.Application.QueryServices;
using GoldMetrics.GoldCheck.Platform.MaterialOperations.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.MaterialOperations.Domain.Model.Queries;
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
    IMaterialQueryService materialQueryService,
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
            material => CreatedAtAction(nameof(GetMaterialById),
                new { batchId = material.BatchId.Value },
                MaterialResourceFromEntityAssembler.ToResourceFromEntity(material)));
    }

    [HttpPut("{batchId}/classify")]
    [SwaggerOperation("Classify Material", "Classify a material batch.", OperationId = "ClassifyMaterial")]
    [SwaggerResponse(200, "Material classified successfully.", typeof(MaterialResource))]
    [SwaggerResponse(404, "The material batch was not found.")]
    [SwaggerResponse(409, "The material batch is already classified.")]
    public async Task<IActionResult> ClassifyMaterial(string batchId, [FromBody] ClassifyMaterialResource resource, CancellationToken cancellationToken)
    {
        var command = new ClassifyMaterialCommand(batchId, resource.Classification);
        var result = await materialCommandService.Handle(command, cancellationToken);

        return MaterialOperationsActionResultAssembler.ToActionResultFromMaterialResult(
            this, result, errorLocalizer, problemDetailsFactory,
            m => Ok(MaterialResourceFromEntityAssembler.ToResourceFromEntity(m)));
    }

    [HttpGet]
    [SwaggerOperation("Get All Materials", "Get all material batches.", OperationId = "GetAllMaterials")]
    [SwaggerResponse(200, "The material batches were found.", typeof(IEnumerable<MaterialResource>))]
    public async Task<IActionResult> GetAllMaterials(CancellationToken cancellationToken)
    {
        var query = new GetAllMaterialsQuery();
        var materials = await materialQueryService.Handle(query, cancellationToken);
        return Ok(materials.Select(MaterialResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{batchId}")]
    [SwaggerOperation("Get Material by Id", "Get a material batch by its identifier.", OperationId = "GetMaterialById")]
    [SwaggerResponse(200, "The material batch was found.", typeof(MaterialResource))]
    [SwaggerResponse(404, "The material batch was not found.")]
    public async Task<IActionResult> GetMaterialById(string batchId, CancellationToken cancellationToken)
    {
        var query = new GetMaterialByIdQuery(batchId);
        var material = await materialQueryService.Handle(query, cancellationToken);

        return MaterialOperationsActionResultAssembler.ToActionResultFromGetMaterialResult(
            this, material, errorLocalizer, problemDetailsFactory,
            m => Ok(MaterialResourceFromEntityAssembler.ToResourceFromEntity(m)));
    }
}
