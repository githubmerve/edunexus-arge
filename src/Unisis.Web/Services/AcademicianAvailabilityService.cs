using Unisis.Web.Models;

namespace Unisis.Web.Services;

public class AcademicianAvailabilityService : IAcademicianAvailabilityService
{
    private readonly IUserRepository _repository;

    public AcademicianAvailabilityService(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<AvailabilitySlot> UpsertAsync(AvailabilitySlot slot, CancellationToken cancellationToken = default)
    {
        await _repository.SaveAvailabilitySlotAsync(slot, cancellationToken);
        return slot;
    }
}
