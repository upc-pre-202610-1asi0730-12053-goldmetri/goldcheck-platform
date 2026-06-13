using GoldMetrics.GoldCheck.Platform.Iam.Application.CommandServices;
using GoldMetrics.GoldCheck.Platform.Iam.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.Iam.Interfaces.Rest.Resources;
using GoldMetrics.GoldCheck.Platform.Iam.Interfaces.Rest.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GoldMetrics.GoldCheck.Platform.Iam.Interfaces.Rest;

[ApiController]
[Route("api/v1/authentication")]
[Produces("application/json")]
public class AuthenticationController(IIamCommandService commandService, IamActionResultAssembler assembler) : ControllerBase
{
    [HttpPost("sign-up")]
    [SwaggerOperation(Summary = "Register a new user", OperationId = "RegisterUser")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserResource resource, CancellationToken ct)
    {
        var command = new RegisterUserCommand(resource.Username, resource.Password, resource.Email, resource.Role);
        var result = await commandService.RegisterUserAsync(command, ct);
        return assembler.ToActionResult(result, this);
    }
}