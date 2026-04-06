using Unisis.Web.Models;

namespace Unisis.Web.Services;

public interface IAcademicianAvailabilityService
{
    Task<AvailabilitySlot> UpsertAsync(AvailabilitySlot slot, CancellationToken cancellationToken = default);
}
