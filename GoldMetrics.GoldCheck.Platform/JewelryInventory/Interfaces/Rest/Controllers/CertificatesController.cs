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
[SwaggerTag("Available Certificate Endpoints.")]
public class CertificatesController(
    IJewelryMaterialCommandService materialCommandService,
    IJewelryCommandService jewelryCommandService,
    IJewelryQueryService jewelryQueryService,
    IStringLocalizer<ErrorMessages> errorLocalizer,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    // POST api/v1/certificates
    [HttpPost]
    [SwaggerOperation("GenerateCertificate",
        "Generates a certificate for a certified jewelry material.")]
    [ProducesResponseType(typeof(JewelryMaterialResource), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> GenerateCertificate(
        [FromBody] GenerateCertificateResource resource,
        CancellationToken cancellationToken)
    {
        var command = GenerateCertificateCommandFromResourceAssembler
            .ToCommandFromResource(resource);
        var result = await materialCommandService.Handle(command, cancellationToken);
        return JewelryInventoryActionResultAssembler.ToActionResultFromMaterialResult(
            this, result, errorLocalizer, problemDetailsFactory,
            material => CreatedAtAction(
                nameof(GetCertificateById),
                new { certificateId = material.CertificateIdRef },
                JewelryMaterialResourceFromEntityAssembler.ToResourceFromEntity(material)));
    }

    // PUT api/v1/certificates/{certificateId}/sign
    [HttpPut("{certificateId}/sign")]
    [SwaggerOperation("SignCertificate",
        "Signs and saves a certificate for a jewelry piece.")]
    [ProducesResponseType(typeof(CertificateResource), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> SignCertificate(
        string certificateId,
        [FromBody] SignCertificateResource resource,
        CancellationToken cancellationToken)
    {
        var command = SignCertificateCommandFromResourceAssembler
            .ToCommandFromResource(certificateId, resource);
        var result = await jewelryCommandService.Handle(command, cancellationToken);
        return JewelryInventoryActionResultAssembler.ToActionResultFromJewelryResult(
            this, result, errorLocalizer, problemDetailsFactory,
            jewelry => Ok(CertificateResourceFromEntityAssembler.ToResourceFromEntity(jewelry)));
    }

    // GET api/v1/certificates/{certificateId}
    [HttpGet("{certificateId}")]
    [SwaggerOperation("GetCertificateById",
        "Returns a signed certificate by its certificate identifier.")]
    [ProducesResponseType(typeof(CertificateResource), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCertificateById(
        string certificateId,
        CancellationToken cancellationToken)
    {
        var query = new GetCertificateByIdQuery(certificateId);
        var jewelry = await jewelryQueryService.Handle(query, cancellationToken);
        if (jewelry is null)
            return NotFound();
        return Ok(CertificateResourceFromEntityAssembler.ToResourceFromEntity(jewelry));
    }
}
