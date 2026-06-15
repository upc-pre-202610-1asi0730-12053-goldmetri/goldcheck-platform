namespace GoldMetrics.GoldCheck.Platform.ReportingNotifications.Domain.Model.ValueObjects;

   public record NotificationType
   {
       private static readonly string[] AllowedValues = ["Alert", "Info", "Warning", "Critical"];
       public NotificationType() => Value = string.Empty;
       public NotificationType(string value)
       {
           if (string.IsNullOrWhiteSpace(value))
               throw new ArgumentException("NotificationType cannot be empty.", nameof(value));
           if (!AllowedValues.Contains(value))
               throw new ArgumentException($"NotificationType must be one of: {string.Join(", ", AllowedValues)}.", nameof(value));
           Value = value;
       }
       public string Value { get; init; }
   }