using GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static ModelBuilder ApplySubscriptionsBillingConfiguration(this ModelBuilder builder)
    {
        builder.Entity<UserSubscription>(entity =>
        {
            entity.HasKey(s => s.Id);
            entity.OwnsOne(s => s.UserId, vo =>
            {
                vo.WithOwner().HasForeignKey("Id");
                vo.Property(v => v.Value).HasColumnName("user_id").IsRequired();
            });
            entity.OwnsOne(s => s.PlanType, vo =>
            {
                vo.WithOwner().HasForeignKey("Id");
                vo.Property(v => v.Value).HasColumnName("plan_type").IsRequired();
            });
            entity.OwnsOne(s => s.BillingCycle, vo =>
            {
                vo.WithOwner().HasForeignKey("Id");
                vo.Property(v => v.Value).HasColumnName("billing_cycle").IsRequired();
            });
            entity.OwnsOne(s => s.SubscriptionStatus, vo =>
            {
                vo.WithOwner().HasForeignKey("Id");
                vo.Property(v => v.Value).HasColumnName("subscription_status").IsRequired();
            });
            entity.Property(s => s.AssignedFeatures)
                .HasConversion(
                    v => string.Join(",", v),
                    v => v.Length == 0 ? new List<string>() : v.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList())
                .HasColumnName("assigned_features");
            entity.Property(s => s.AccessGranted).IsRequired();
            entity.Property(s => s.RequestedDowngradePlan);
            entity.Property(s => s.Status).IsRequired();
            entity.HasMany(s => s.Invoices)
                .WithOne()
                .HasForeignKey(i => i.UserSubscriptionId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<Invoice>(entity =>
        {
            entity.HasKey(i => i.Id);
            entity.OwnsOne(i => i.InvoiceId, vo =>
            {
                vo.WithOwner().HasForeignKey("Id");
                vo.Property(v => v.Value).HasColumnName("invoice_id").IsRequired();
            });
            entity.OwnsOne(i => i.Amount, vo =>
            {
                vo.WithOwner().HasForeignKey("Id");
                vo.Property(v => v.Value).HasColumnName("amount").IsRequired();
            });
            entity.Property(i => i.Status).IsRequired();
            entity.Property(i => i.UserSubscriptionId).IsRequired();
        });

        return builder;
    }
}