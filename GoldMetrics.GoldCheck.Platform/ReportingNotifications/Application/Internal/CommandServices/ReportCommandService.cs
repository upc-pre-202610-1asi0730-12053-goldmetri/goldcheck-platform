using GoldMetrics.GoldCheck.Platform.ReportingNotifications.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.ReportingNotifications.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.Shared.Application.Model;
using Microsoft.EntityFrameworkCore;

namespace GoldMetrics.GoldCheck.Platform.ReportingNotifications.Application.Internal.CommandServices;

public class ReportCommandService
{
    public async Task<Result<Report>> Handle(LoadAccidentDataCommand command, CancellationToken cancellationToken)
    {
        var report = await FindReport(command.Id, cancellationToken);
        if (report is null) return NotFound();
        try
        {
            report.LoadAccidentData(command);
            reportRepository.Update(report);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<Report>.Success(report);
        }
        catch (OperationCanceledException) { return Cancelled(); }
        catch (DbUpdateException) { return DbError(); }
        catch (Exception) { return ServerError(); }
    }
}