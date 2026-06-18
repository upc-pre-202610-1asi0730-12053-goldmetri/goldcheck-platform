namespace GoldMetrics.GoldCheck.Platform.Analytics.Domain.Model.Queries;

public record GetProductionDataByPeriodQuery(DateTimeOffset Start, DateTimeOffset End);