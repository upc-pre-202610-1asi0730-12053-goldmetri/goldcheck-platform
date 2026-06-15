using System.Net.Mime;
    using GoldMetrics.GoldCheck.Platform.ReportingNotifications.Application.CommandServices;
    using GoldMetrics.GoldCheck.Platform.ReportingNotifications.Application.QueryServices;
    using GoldMetrics.GoldCheck.Platform.ReportingNotifications.Domain.Model.Commands;
    using GoldMetrics.GoldCheck.Platform.ReportingNotifications.Domain.Model.Queries;
    using GoldMetrics.GoldCheck.Platform.ReportingNotifications.Interfaces.Rest.Resources;
    using GoldMetrics.GoldCheck.Platform.ReportingNotifications.Interfaces.Rest.Transform;
    using GoldMetrics.GoldCheck.Platform.Shared.Interfaces.Rest.ProblemDetails;
    using GoldMetrics.GoldCheck.Platform.Shared.Resources.Errors;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Localization;
    using Swashbuckle.AspNetCore.Annotations;

    namespace GoldMetrics.GoldCheck.Platform.ReportingNotifications.Interfaces.Rest;

    [ApiController]
    [Route("api/v1/notifications")]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerTag("Available Notification Endpoints.")]
    public class NotificationsController(
        INotificationCommandService notificationCommandService,
        INotificationQueryService notificationQueryService,
        IStringLocalizer<ErrorMessages> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory)
        : ControllerBase
    {
        [HttpPost]
        [SwaggerOperation("Request Notification", "Create a new notification request.", OperationId = "RequestNotification")]
        [SwaggerResponse(201, "Notification requested.", typeof(NotificationResource))]
        [SwaggerResponse(400, "Invalid request data.")]
        public async Task<IActionResult> RequestNotification([FromBody] RequestNotificationResource resource, CancellationToken cancellationToken)
        {
            var command = new RequestNotificationCommand(resource.RecipientId, resource.Type, resource.Message);
            var result = await notificationCommandService.Handle(command, cancellationToken);
            return ReportingNotificationsActionResultAssembler.ToActionResultFromNotificationResult(
                this, result, errorLocalizer, problemDetailsFactory,
                n => CreatedAtAction(nameof(GetNotificationById), new { notificationId = n.Id },
                    NotificationResourceFromEntityAssembler.ToResourceFromEntity(n)));
        }
        
        [HttpPut("{notificationId:int}/send")]
        [SwaggerOperation("Send Notification", "Send a requested notification.", OperationId = "SendNotification")]
        [SwaggerResponse(200, "Notification sent.", typeof(NotificationResource))]
        [SwaggerResponse(404, "Notification not found.")]
        public async Task<IActionResult> SendNotification(int notificationId, CancellationToken cancellationToken)
        {
            var result = await notificationCommandService.Handle(new SendNotificationCommand(notificationId), cancellationToken);
            return ReportingNotificationsActionResultAssembler.ToActionResultFromNotificationResult(
                this, result, errorLocalizer, problemDetailsFactory,
                n => Ok(NotificationResourceFromEntityAssembler.ToResourceFromEntity(n)));
        }
        
        [HttpGet("{notificationId:int}")]
        [SwaggerOperation("Get Notification By Id", "Get a notification by its identifier.", OperationId = "GetNotificationById")]
        [SwaggerResponse(200, "Notification found.", typeof(NotificationResource))]
        [SwaggerResponse(404, "Notification not found.")]
        
        public async Task<IActionResult> GetNotificationById(int notificationId, CancellationToken cancellationToken)
        {
            var notification = await notificationQueryService.Handle(new GetNotificationByIdQuery(notificationId), cancellationToken);
            return ReportingNotificationsActionResultAssembler.ToActionResultFromGetNotificationResult(
                this, notification, errorLocalizer, problemDetailsFactory,
                n => Ok(NotificationResourceFromEntityAssembler.ToResourceFromEntity(n)));
        }
    }