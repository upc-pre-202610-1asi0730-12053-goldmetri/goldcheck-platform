using System.Net.Mime;
using GoldMetrics.GoldCheck.Platform.JewelryInventory.Application.CommandServices;
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
            material => Created(string.Empty,
                JewelryMaterialResourceFromEntityAssembler.ToResourceFromEntity(material)));
    }
}
