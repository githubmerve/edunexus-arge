using Unisis.Web.Models;

namespace Unisis.Web.Services;

public class ExcuseRequestService : IExcuseRequestService
{
    private readonly IUserRepository _repository;

    public ExcuseRequestService(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<ExcuseRequest> CreateAsync(ExcuseRequest request, CancellationToken cancellationToken = default)
    {
        request.Status = "Pending";
        request.CreatedAtUtc = DateTime.UtcNow;
        request.UpdatedAtUtc = request.CreatedAtUtc;

        await _repository.SaveExcuseRequestAsync(request, cancellationToken);
        return request;
    }
}
