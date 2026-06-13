using GoldMetrics.GoldCheck.Platform.Iam.Application.CommandServices;
using GoldMetrics.GoldCheck.Platform.Iam.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.Iam.Interfaces.Rest.Resources;
using GoldMetrics.GoldCheck.Platform.Iam.Interfaces.Rest.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GoldMetrics.GoldCheck.Platform.Iam.Interfaces.Rest;

[ApiController]
[Route("api/v1/users")]
[Produces("application/json")]
public class UsersController(
    IIamCommandService commandService,
    IamActionResultAssembler assembler) : ControllerBase
{
    [HttpPut("{userId}/profile")]
    [SwaggerOperation(Summary = "Update user profile", OperationId = "UpdateProfile")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateProfile([FromRoute] string userId, [FromBody] UpdateProfileResource resource, CancellationToken ct)
    {
        var command = new UpdateProfileCommand(userId, resource.Username, resource.Email);
        var result = await commandService.UpdateProfileAsync(command, ct);
        return assembler.ToActionResult(result, this);
    }
}