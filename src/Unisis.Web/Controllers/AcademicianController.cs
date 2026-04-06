using Microsoft.AspNetCore.Mvc;
using Unisis.Web.Models;
using Unisis.Web.Services;

namespace Unisis.Web.Controllers;

[ApiController]
[Route("api/academician")]
public class AcademicianController : ControllerBase
{
    private readonly IAcademicianAvailabilityService _availabilityService;

    public AcademicianController(IAcademicianAvailabilityService availabilityService)
    {
        _availabilityService = availabilityService;
    }

    [HttpPost("availability")]
    public async Task<IActionResult> UpsertAvailability(
        [FromBody] AvailabilitySlot slot,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(slot.AcademicianId) ||
            string.IsNullOrWhiteSpace(slot.Date) ||
            string.IsNullOrWhiteSpace(slot.StartTime) ||
            string.IsNullOrWhiteSpace(slot.EndTime))
        {
            return BadRequest("AcademicianId, date, startTime ve endTime zorunludur.");
        }

        var saved = await _availabilityService.UpsertAsync(slot, cancellationToken);
        return Ok(saved);
    }
}
