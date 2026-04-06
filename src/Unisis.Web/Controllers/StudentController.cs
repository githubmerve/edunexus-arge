using Microsoft.AspNetCore.Mvc;
using Unisis.Web.Models;
using Unisis.Web.Services;

namespace Unisis.Web.Controllers;

[ApiController]
[Route("api/student")]
public class StudentController : ControllerBase
{
    private readonly IIssueReportService _issueReportService;
    private readonly IExcuseRequestService _excuseRequestService;

    public StudentController(
        IIssueReportService issueReportService,
        IExcuseRequestService excuseRequestService)
    {
        _issueReportService = issueReportService;
        _excuseRequestService = excuseRequestService;
    }

    [HttpPost("issue-reports")]
    public async Task<IActionResult> CreateIssueReport(
        [FromBody] IssueReport report,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(report.StudentId) ||
            string.IsNullOrWhiteSpace(report.Title) ||
            string.IsNullOrWhiteSpace(report.Location))
        {
            return BadRequest("StudentId, title ve location alanlari zorunludur.");
        }

        var created = await _issueReportService.CreateAsync(report, cancellationToken);
        return Ok(created);
    }

    [HttpPost("excuse-requests")]
    public async Task<IActionResult> CreateExcuseRequest(
        [FromBody] ExcuseRequest request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.StudentId) ||
            string.IsNullOrWhiteSpace(request.Title))
        {
            return BadRequest("StudentId ve title alanlari zorunludur.");
        }

        var created = await _excuseRequestService.CreateAsync(request, cancellationToken);
        return Ok(created);
    }
}
