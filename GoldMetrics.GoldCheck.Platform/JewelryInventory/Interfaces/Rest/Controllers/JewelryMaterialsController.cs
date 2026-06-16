using System.Net.Mime;
using GoldMetrics.GoldCheck.Platform.JewelryInventory.Application.CommandServices;
using GoldMetrics.GoldCheck.Platform.JewelryInventory.Application.QueryServices;
using GoldMetrics.GoldCheck.Platform.JewelryInventory.Domain.Model.Queries;
using GoldMetrics.GoldCheck.Platform.JewelryInventory.Interfaces.Rest.Resources;
using GoldMetrics.GoldCheck.Platform.JewelryInventory.Interfaces.Rest.Transform;
using GoldMetrics.GoldCheck.Platform.Shared.Resources.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;
using ProblemDetailsFactory =
    GoldMetrics.GoldCheck.Platform.Shared.Interfaces.Rest.ProblemDetails.ProblemDetailsFactory;

namespace GoldMetrics.GoldCheck.Platform.JewelryInventory.Interfaces.Rest.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Jewelry Material Endpoints.")]
public class JewelryMaterialsController(
    IJewelryMaterialCommandService materialCommandService,
    IJewelryMaterialQueryService materialQueryService,
    IStringLocalizer<ErrorMessages> errorLocalizer,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    // POST api/v1/jewelry-materials
    [HttpPost]
    [SwaggerOperation("RegisterNonCertifiedMaterial",
        "Registers a new non-certified material in the jewelry inventory.")]
    [ProducesResponseType(typeof(JewelryMaterialResource), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> RegisterMaterial(
        [FromBody] CreateMaterialResource resource,
        CancellationToken cancellationToken)
    {
        var command = RegisterNonCertifiedMaterialCommandFromResourceAssembler
            .ToCommandFromResource(resource);
        var result = await materialCommandService.Handle(command, cancellationToken);
        return JewelryInventoryActionResultAssembler.ToActionResultFromMaterialResult(
            this, result, errorLocalizer, problemDetailsFactory,
            material => CreatedAtAction(
                nameof(GetMaterialById),
                new { materialId = material.MaterialId.Value },
                JewelryMaterialResourceFromEntityAssembler.ToResourceFromEntity(material)));
    }

    // GET api/v1/jewelry-materials/{materialId}
    [HttpGet("{materialId}")]
    [SwaggerOperation("GetMaterialById",
        "Returns a jewelry material by its material identifier.")]
    [ProducesResponseType(typeof(JewelryMaterialResource), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMaterialById(
        string materialId,
        CancellationToken cancellationToken)
    {
        var query = new GetMaterialByIdQuery(materialId);
        var material = await materialQueryService.Handle(query, cancellationToken);
        if (material is null)
            return NotFound();
        return Ok(JewelryMaterialResourceFromEntityAssembler.ToResourceFromEntity(material));
    }
}
