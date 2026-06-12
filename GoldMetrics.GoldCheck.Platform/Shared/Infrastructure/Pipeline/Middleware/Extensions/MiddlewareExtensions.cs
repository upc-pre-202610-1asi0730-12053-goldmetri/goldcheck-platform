using GoldMetrics.GoldCheck.Platform.Shared.Infrastructure.Pipeline.Middleware.Components;

namespace GoldMetrics.GoldCheck.Platform.Shared.Infrastructure.Pipeline.Middleware.Extensions;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<GlobalExceptionHandlerMiddleware>();
    }
}
