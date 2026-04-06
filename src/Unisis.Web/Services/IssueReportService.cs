using Unisis.Web.Models;

namespace Unisis.Web.Services;

public class IssueReportService : IIssueReportService
{
    private readonly IUserRepository _repository;

    public IssueReportService(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<IssueReport> CreateAsync(IssueReport report, CancellationToken cancellationToken = default)
    {
        report.Status = "Open";
        report.CreatedAtUtc = DateTime.UtcNow;
        report.UpdatedAtUtc = report.CreatedAtUtc;

        await _repository.SaveIssueReportAsync(report, cancellationToken);
        return report;
    }
}
