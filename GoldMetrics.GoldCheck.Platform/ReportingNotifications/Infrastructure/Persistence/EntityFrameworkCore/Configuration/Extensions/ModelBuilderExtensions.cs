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
        builder.Entity<Report>().Property(r => r.DownloadedByUserId).HasMaxLength(100);
        builder.Entity<Report>().Property(r => r.Status).IsRequired().HasMaxLength(50);
        
        // Notification
        builder.Entity<Notification>().HasKey(n => n.Id);
        builder.Entity<Notification>().Property(n => n.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Notification>().OwnsOne(n => n.RecipientId, rid =>
        {
            rid.WithOwner().HasForeignKey("Id");
            rid.Property(x => x.Value).HasColumnName("RecipientId").IsRequired().HasMaxLength(100);
        });
        builder.Entity<Notification>().OwnsOne(n => n.NotificationType, nt =>
        {
            nt.WithOwner().HasForeignKey("Id");
            nt.Property(x => x.Value).HasColumnName("NotificationType").IsRequired().HasMaxLength(50);
        });
        builder.Entity<Notification>().OwnsOne(n => n.NotificationStatus, ns =>
        {
            ns.WithOwner().HasForeignKey("Id");
            ns.Property(x => x.Value).HasColumnName("NotificationStatus").IsRequired().HasMaxLength(50);
        });
        builder.Entity<Notification>().Property(n => n.Message).IsRequired().HasMaxLength(1000);
        builder.Entity<Notification>().Property(n => n.Status).IsRequired().HasMaxLength(50);
    }
    
}