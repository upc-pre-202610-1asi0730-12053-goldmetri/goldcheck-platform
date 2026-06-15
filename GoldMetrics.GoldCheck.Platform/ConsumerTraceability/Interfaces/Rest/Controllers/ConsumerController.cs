using System.Net.Mime;
using GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Application.CommandServices;
using GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Application.QueryServices;
using GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Domain.Model.Queries;
using GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Interfaces.Rest.Resources;
using GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Interfaces.Rest.Transform;
using GoldMetrics.GoldCheck.Platform.Shared.Resources.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;
using ProblemDetailsFactory =
    GoldMetrics.GoldCheck.Platform.Shared.Interfaces.Rest.ProblemDetails.ProblemDetailsFactory;

namespace GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Interfaces.Rest.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Consumer Traceability Endpoints.")]
public class ConsumerController(
    IJewelryProductCommandService productCommandService,
    IStringLocalizer<ErrorMessages> errorLocalizer,
    ProblemDetailsFactory problemDetailsFactory,
    IJewelryProductQueryService productQueryService
    )
    : ControllerBase
{
    // POST api/v1/consumer/scan
    [HttpPost("scan")]
    [SwaggerOperation("ScanProductQR",
        "Scans a product QR code and registers the consumer interaction.")]
    [ProducesResponseType(typeof(JewelryProductResource), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ScanProductQR(
        [FromBody] ScanProductQRResource resource,
        CancellationToken cancellationToken)
    {
        var command = ScanProductQRCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await productCommandService.Handle(command, cancellationToken);
        return ConsumerTraceabilityActionResultAssembler.ToActionResultFromProductResult(
            this, result, errorLocalizer, problemDetailsFactory,
            product => CreatedAtAction(
                nameof(GetProductByQR),
                new { qrCode = product.QRCode.Value },
                JewelryProductResourceFromEntityAssembler.ToResourceFromEntity(product)));
    }
    // GET api/v1/consumer/products/{qrCode}
    [HttpGet("products/{qrCode}")]
    [SwaggerOperation("GetProductByQR",
        "Returns the product data associated with a QR code.")]
    [ProducesResponseType(typeof(JewelryProductResource), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProductByQR(
        string qrCode,
        CancellationToken cancellationToken)
    {
        var query = new GetProductByQRQuery(qrCode);
        var product = await productQueryService.Handle(query, cancellationToken);
        if (product is null) return NotFound();
        return Ok(JewelryProductResourceFromEntityAssembler.ToResourceFromEntity(product));
    }
}