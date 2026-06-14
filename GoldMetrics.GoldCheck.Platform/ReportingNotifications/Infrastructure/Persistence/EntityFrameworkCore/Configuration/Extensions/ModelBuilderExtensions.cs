using GoldMetrics.GoldCheck.Platform.ReportingNotifications.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace GoldMetrics.GoldCheck.Platform.ReportingNotifications.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyReportingNotificationsConfiguration(this ModelBuilder builder)
    {
        // Report
        builder.Entity<Report>().HasKey(r => r.Id);
        builder.Entity<Report>().Property(r => r.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Report>().OwnsOne(r => r.SupervisorId, sid =>
        {
            sid.WithOwner().HasForeignKey("Id");
            sid.Property(x => x.Value).HasColumnName("SupervisorId").IsRequired().HasMaxLength(100);
        });
        builder.Entity<Report>().Property(r => r.IncidentId).IsRequired().HasMaxLength(100);
        builder.Entity<Report>().OwnsOne(r => r.ReportStatus, rs =>
        {
            rs.WithOwner().HasForeignKey("Id");
            rs.Property(x => x.Value).HasColumnName("ReportStatus").IsRequired().HasMaxLength(50);
        });
        builder.Entity<Report>().OwnsOne(r => r.ReportFormat, rf =>
        {
            rf.WithOwner().HasForeignKey("Id");
            rf.Property(x => x.Value).HasColumnName("ReportFormat").IsRequired().HasMaxLength(20);
        });
        builder.Entity<Report>().Property(r => r.Status).IsRequired().HasMaxLength(50);
    }
}