using GoldMetrics.GoldCheck.Platform.Analytics.Domain.Model.Commands;

namespace GoldMetrics.GoldCheck.Platform.Analytics.Application.Internal.CommandServices;

public partial class AnalyticsCommandService
{
    
    
    //Paso 16 de la guia de analytics que no entendi
    partial void Handle(ViewRouteProgressCommand command, CancellationToken cancellationToken);

}