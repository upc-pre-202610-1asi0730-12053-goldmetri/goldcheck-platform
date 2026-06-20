using GoldMetrics.GoldCheck.Platform.FleetOperations.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using GoldMetrics.GoldCheck.Platform.JewelryInventory.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using GoldMetrics.GoldCheck.Platform.MaterialOperations.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using GoldMetrics.GoldCheck.Platform.Analytics.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using GoldMetrics.GoldCheck.Platform.IncidentManagement.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using GoldMetrics.GoldCheck.Platform.ReportingNotifications.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using GoldMetrics.GoldCheck.Platform.AssetMaintenance.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using GoldMetrics.GoldCheck.Platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using GoldMetrics.GoldCheck.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using GoldMetrics.GoldCheck.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace GoldMetrics.GoldCheck.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.AddInterceptors(new AuditableEntityInterceptor());
        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // BC1 — Fleet Operations
        builder.ApplyFleetOperationsConfiguration();

        // BC2 — Material Operations
        builder.ApplyMaterialOperationsConfiguration();

        // BC3 — Jewelry Inventory & Certification
        builder.ApplyJewelryInventoryConfiguration();

        // BC4 — Consumer Traceability
        builder.ApplyConsumerTraceabilityConfiguration();

        // BC5 — Monitoring & Telemetry
        builder.ApplyMonitoringTelemetryConfiguration();

        // BC6 — Analytics
        builder.ApplyAnalyticsConfiguration();

        // BC7 — Incident Management
        builder.ApplyIncidentManagementConfiguration();

        // BC8 — Reporting & Notifications
        builder.ApplyReportingNotificationsConfiguration();

        // BC9 — Asset & Maintenance Management
        builder.ApplyAssetMaintenanceConfiguration();

        // BC10 — IAM
        builder.ApplyIAMConfiguration();

        // BC11 — Subscriptions & Billing
        builder.ApplySubscriptionsBillingConfiguration();

        // UseSnakeCaseNamingConvention ALWAYS last
        builder.UseSnakeCaseNamingConvention();
    }
}
