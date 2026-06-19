using System.Net.Mime;
using GoldMetrics.GoldCheck.Platform.FleetOperations.Application.CommandServices;
using GoldMetrics.GoldCheck.Platform.FleetOperations.Interfaces.Rest.Resources;
using GoldMetrics.GoldCheck.Platform.FleetOperations.Interfaces.Rest.Transform;
using GoldMetrics.GoldCheck.Platform.Shared.Interfaces.Rest.ProblemDetails;
using GoldMetrics.GoldCheck.Platform.Shared.Resources.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;

namespace GoldMetrics.GoldCheck.Platform.FleetOperations.Interfaces.Rest;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Vehicle Endpoints.")]
public class VehiclesController(
    IVehicleCommandService vehicleCommandService,
    IStringLocalizer<ErrorMessages> errorLocalizer,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    [HttpPost]
    [SwaggerOperation("Assign Vehicle", "Assign a vehicle to an operator.", OperationId = "AssignVehicle")]
    [SwaggerResponse(201, "The vehicle was assigned.", typeof(VehicleResource))]
    [SwaggerResponse(409, "The vehicle is already assigned.")]
    public async Task<IActionResult> AssignVehicle([FromBody] CreateVehicleResource resource, CancellationToken cancellationToken)
    {
        var command = AssignVehicleCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await vehicleCommandService.Handle(command, cancellationToken);

        return FleetOperationsActionResultAssembler.ToActionResultFromVehicleResult(
            this, result, errorLocalizer, problemDetailsFactory,
            vehicle => Created(string.Empty, VehicleResourceFromEntityAssembler.ToResourceFromEntity(vehicle)));
    }
}
