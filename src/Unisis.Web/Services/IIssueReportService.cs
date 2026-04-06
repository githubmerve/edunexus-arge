using Unisis.Web.Models;

namespace Unisis.Web.Services;

public interface IIssueReportService
{
    Task<IssueReport> CreateAsync(IssueReport report, CancellationToken cancellationToken = default);
}
